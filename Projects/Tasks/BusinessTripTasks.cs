using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using CrazyAppsStudio.Delegacje.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace CrazyAppsStudio.Delegacje.Tasks
{
	public class BusinessTripsTasks
	{
		private Repositories repo;

		public BusinessTripsTasks()
        {
            repo = new Repositories();
        }

        public int CreateNewBusinessTrip(BusinessTripDTO businessTrip, string userName)
        {
			User user = repo.Users.UsersQueryable.FirstOrDefault(u => u.UserName == userName);

            Subsistence sub = null;
            if (businessTrip.Subsistence != null)
            {
                sub = repo.Subsistences.Create(new Subsistence()
                {
                    StartDate = DateExtensions.ParseAppString(businessTrip.Subsistence.StartDate),
                    EndDate = DateExtensions.ParseAppString(businessTrip.Subsistence.EndDate),
                    City = businessTrip.Subsistence.City,
                    Country = repo.Dictionaries.GetCountry(businessTrip.Subsistence.CountryId)
                });
                
                //trip.Subsistence = sub;                

                List<SubsistenceDay> days = new List<SubsistenceDay>();
                foreach (SubsistenceDayDTO dayDto in businessTrip.Subsistence.Days)
                {
                    days.Add(new SubsistenceDay()
                    {
                        Amount = dayDto.Amount,
                        AmountPLN = dayDto.AmountPLN,
                        Breakfast = dayDto.Breakfast,
                        Date = DateExtensions.ParseAppString(dayDto.Date),
                        Dinner = dayDto.Dinner,
                        Supper = dayDto.Supper,
                        Night = dayDto.Night,
                        Subsistence = sub
                    });
                }

                repo.SubsistenceDays.CreateSet(days);                
            }            

            BusinessTrip trip = repo.BusinessTrips.Create(new BusinessTrip() {
				Title = businessTrip.Title,
				Date = businessTrip.Date.ParseAppString(),
				BusinessReason = businessTrip.BusinessReason,
				BusinessPurpose = businessTrip.BusinessPurpose,
				Notes = businessTrip.Notes,
				User = user,
                Subsistence = sub				
			});

			List<Expense> expenses = new List<Expense>();
			if (businessTrip.Expenses != null)
			{
				foreach (ExpenseDTO expDto in businessTrip.Expenses)
				{
					Expense expense = new Expense();
					expense.Trip = trip;
					expense.Type = repo.Dictionaries.GetExpenseType(expDto.ExpenseTypeId);
					expense.Date = expDto.Date.ParseAppString();
					expense.City = expDto.City;
					expense.Amount = expDto.Amount;
					expense.Country = repo.Dictionaries.GetCountry(expDto.CountryId);
					expense.CurrencyCode = expDto.CurrencyCode;
					expense.ExchangeRate = expDto.ExchangeRate;
					CurrencyRate systemRate = repo.Currencies.GetCurrencyRate(expense.CurrencyCode, expense.Date.Date);
					if (Math.Abs(systemRate.ExchangeRate - expense.ExchangeRate) > 0.0001)
						expense.ExchangeRateModifiedByUser = true;
					else
						expense.ExchangeRateModifiedByUser = false;
					
					expense.VATRate = expDto.VATRate;
					expense.Notes = expDto.Notes;
                    expense.DoNotReturn = expDto.DoNotReturn;
                    expense.DocumentType = repo.Dictionaries.GetExpenseDocumentType(expDto.ExpenseDocumentTypeId);
					expenses.Add(expense);
				}

				repo.Expenses.CreateSet(expenses);
			}

           

            if (businessTrip.MileageAllowances != null)
			{
				List<MileageAllowance> mileageAllowances = new List<MileageAllowance>();
				foreach (MileageAllowanceDTO maDto in businessTrip.MileageAllowances)
				{
					MileageAllowance ma = new MileageAllowance();
					ma.Trip = trip;
					ma.Date = maDto.Date.ParseAppString();
					ma.Distance = maDto.Distance;
					ma.Amount = maDto.Amount;
					ma.Notes = maDto.Notes;
					ma.Type = repo.Dictionaries.GetVehicleType(maDto.VehicleTypeId);

					mileageAllowances.Add(ma);
				}

				repo.MileageAllowances.CreateSet(mileageAllowances);
			}

			this.repo.SaveChanges();
			return trip.Id;
        }

        public IEnumerable<BusinessTripSearchItemDTO> GetForUser(string user)
        {
            return repo.BusinessTrips.GetForUser(user);
        }

		public void UpdateBusinessTrip(BusinessTripDTO businessTripDto, string userName)
		{
			BusinessTrip trip = repo.BusinessTrips.GetById(businessTripDto.Id.Value);
            User user = repo.Users.UsersQueryable.FirstOrDefault(u => u.UserName == userName);

            trip.Title = businessTripDto.Title;
			trip.Date = Convert.ToDateTime(businessTripDto.Date);
			trip.BusinessReason = businessTripDto.BusinessReason;
			trip.BusinessPurpose = businessTripDto.BusinessPurpose;
			trip.Notes = businessTripDto.Notes;
            trip.User = user;

			UpdateBusinessTripExpenses(trip, businessTripDto);
			UpdateBusinessTripSubsistences(trip, businessTripDto);
			UpdateBusinessTripMileageAllowances(trip, businessTripDto);

			this.repo.SaveChanges();
		}

		public void UpdateBusinessTripExpenses(BusinessTrip trip, BusinessTripDTO businessTripDto)
		{
			if (businessTripDto.Expenses != null)
			{
				foreach (Expense expense in trip.Expenses
					.Where(e => businessTripDto.Expenses
						.Any(edto => edto.ExpenseId == e.Id)))
				{//Update expenses that exist both in database and in dto					
					ExpenseDTO edto = businessTripDto.Expenses.First(expenseDto => expenseDto.ExpenseId == expense.Id);
					expense.Type = repo.Dictionaries.GetExpenseType(edto.ExpenseTypeId);
					expense.Date = edto.Date.ParseAppString();
					expense.City = edto.City;
					expense.Amount = edto.Amount;
					//expense.CountryId = expDto.CountryId;
					expense.Country = repo.Dictionaries.GetCountry(edto.CountryId);
					expense.CurrencyCode = edto.CurrencyCode;
					expense.ExchangeRate = edto.ExchangeRate;
					expense.ExchangeRateModifiedByUser = edto.ExchangeRateModifiedByUser;
					expense.VATRate = edto.VATRate;
					expense.Notes = edto.Notes;
				}

				//Remove those that exist in db but don't exist in dto
				repo.Expenses.RemoveSet(trip.Expenses
					.Where(e => !businessTripDto.Expenses
						.Any(edto => edto.ExpenseId == e.Id)));

				foreach (ExpenseDTO expDto in businessTripDto.Expenses.Where(edto => !trip.Expenses.Any(exp => exp.Id == edto.ExpenseId)))
				{//Add those that exist in dto but don't exist in db
					Expense expense = new Expense();
					expense.Trip = trip;
					expense.Type = repo.Dictionaries.GetExpenseType(expDto.ExpenseTypeId);
					expense.Date = expDto.Date.ParseAppString();
					expense.City = expDto.City;
					expense.Amount = expDto.Amount;
					//expense.CountryId = expDto.CountryId;
					expense.Country = repo.Dictionaries.GetCountry(expDto.CountryId);
					expense.CurrencyCode = expDto.CurrencyCode;
					expense.ExchangeRate = expDto.ExchangeRate;
					expense.ExchangeRateModifiedByUser = expDto.ExchangeRateModifiedByUser;
					expense.VATRate = expDto.VATRate;
					expense.Notes = expDto.Notes;
                    expense.DoNotReturn = expDto.DoNotReturn;
                    expense.DocumentType = repo.Dictionaries.GetExpenseDocumentType(expDto.ExpenseDocumentTypeId);
					trip.Expenses.Add(expense);
				}
			}
		}

		public void UpdateBusinessTripSubsistences(BusinessTrip trip, BusinessTripDTO businessTripDto)
		{
			//if (businessTripDto.Subsistences != null)
			//{
			//	foreach (Subsistence sub in trip.Subsistences
			//		.Where(s => businessTripDto.Subsistences
			//			.Any(sdto => sdto.Id == s.Id)))
			//	{//Update subsistences that exist both in database and in dto					
			//		SubsistenceDTO sdto = businessTripDto.Subsistences.First(subsistenceDto => subsistenceDto.Id == sub.Id);
			//		sub.Trip = trip;
			//		sub.Date = DateExtensions.ParseAppString(sdto.Date);
			//		sub.City = sdto.City;
			//		sub.Country = repo.Dictionaries.GetCountry(sdto.CountryId);
			//		sub.AccomodationCount = sdto.AccomodationCount;
   //                 sub.Breakfast = sdto.Breakfast;
   //                 sub.Dinner = sdto.Dinner;
   //                 sub.Supper = sdto.Supper;
   //                 sub.Amount = sdto.Amount;
   //             }

   //             //Remove those that exist in db but don't exist in dto
   //             repo.Subsistences.RemoveSet(trip.Subsistences
			//		.Where(s => !businessTripDto.Subsistences
			//			.Any(sdto => sdto.Id == s.Id)));

			//	foreach (SubsistenceDTO subDto in businessTripDto.Subsistences.Where(sdto => !trip.Subsistences.Any(s => s.Id == sdto.Id)))
			//	{//Add those that exist in dto but don't exist in db					
			//		Subsistence sub = new Subsistence();
			//		sub.Trip = trip;
			//		sub.Date = DateExtensions.ParseAppString(subDto.Date);
			//		sub.City = subDto.City;
			//		sub.Country = repo.Dictionaries.GetCountry(subDto.CountryId);
			//		sub.AccomodationCount = subDto.AccomodationCount;
   //                 sub.Breakfast = subDto.Breakfast;
   //                 sub.Dinner = subDto.Dinner;
   //                 sub.Supper = subDto.Supper;
   //                 sub.Amount = subDto.Amount;

   //                 trip.Subsistences.Add(sub);
			//	}
			//}
		}

		public void UpdateBusinessTripMileageAllowances(BusinessTrip trip, BusinessTripDTO businessTripDto)
		{
			if (businessTripDto != null)
			{
				foreach (MileageAllowance allowance in trip.MileageAllowances
					.Where(m => businessTripDto.MileageAllowances
						.Any(mdto => mdto.id == m.Id)))
				{//Update mileage allowances that exist both in database and in dto					
					MileageAllowanceDTO mdto = businessTripDto.MileageAllowances.First(m => m.id == allowance.Id);
					allowance.Type = repo.Dictionaries.GetVehicleType(mdto.VehicleTypeId);
					allowance.Date = mdto.Date.ParseAppString();					
					allowance.Amount = mdto.Amount;					
					allowance.Distance = mdto.Distance;					
					allowance.Notes = mdto.Notes;
				}

				//Remove those that exist in db but don't exist in dto
				repo.MileageAllowances.RemoveSet(trip.MileageAllowances
					.Where(m => !businessTripDto.MileageAllowances
						.Any(mdto => mdto.id == m.Id)));

				foreach (MileageAllowanceDTO milDto in businessTripDto.MileageAllowances.Where(mdto => !trip.MileageAllowances.Any(ma => ma.Id == mdto.id)))
				{//Add those that exist in dto but don't exist in db
					MileageAllowance allowance = new MileageAllowance();
					allowance.Trip = trip;					
					allowance.Date = milDto.Date.ParseAppString();					
					allowance.Amount = milDto.Amount;
					allowance.Distance = milDto.Distance;
					allowance.Type = repo.Dictionaries.GetVehicleType(milDto.VehicleTypeId);					
					allowance.Notes = milDto.Notes;
					trip.MileageAllowances.Add(allowance);
				}
			}
		}
		
		public void DeleteBusinessTrip(int businessTripId)
		{
			this.repo.BusinessTrips.Remove(businessTripId);
			this.repo.SaveChanges();
		}

		public BusinessTrip GetBusinessTrip(int businessTripId)
		{
			return this.repo.BusinessTrips.GetById(businessTripId);
		}
	}
}
