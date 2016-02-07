using CrazyAppsStudio.Delegacje.Domain.DTO;
using CrazyAppsStudio.Delegacje.Domain.Entities;
using CrazyAppsStudio.Delegacje.Domain.Entities.Identity;
using CrazyAppsStudio.Delegacje.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyAppsStudio.Delegacje.Tasks
{
	public class BusinessTripsTasks
	{
		private Repositories repo;

		public BusinessTripsTasks()
        {
            repo = new Repositories();
        }

        public void CreateNewBusinessTrip(AddBusinessTripDTO businessTrip)
        {
			User user = repo.Users.UsersQueryable.FirstOrDefault(u => u.Id == 1); //TODO Add log in, take user data from Identity - MS 07/02/2016

			BusinessTrip trip = repo.BusinessTrips.Create(new BusinessTrip() {
				Title = businessTrip.Title,
				Date = businessTrip.Date,
				BusinessReason = businessTrip.BusinessReason,
				BusinessPurpose = businessTrip.BusinessPurpose,
				Notes = businessTrip.Notes,
				User = user				
			});

			List<Expense> expenses = new List<Expense>();
			if (businessTrip.Expenses != null)
			{
				foreach (ExpenseDTO expDto in businessTrip.Expenses)
				{
					Expense expense = new Expense();
					expense.Trip = trip;
					expense.Type = repo.Dictionaries.GetExpenseType(expDto.ExpenseTypeId);
					expense.Date = expDto.Date;
					expense.City = expDto.City;
					expense.Amount = expDto.Amount;
					//expense.CountryId = expDto.CountryId;
					expense.Country = repo.Dictionaries.GetCountry(expDto.CountryId);
					expense.CurrencyCode = expDto.CurrencyCode;
					expense.ExchangeRate = expDto.ExchangeRate;
					expense.ExchangeRateModifiedByUser = expDto.ExchangeRateModifiedByUser;
					expense.VATRate = expDto.VATRate;
					expense.Notes = expDto.Notes;
					expenses.Add(expense);
				}

				repo.Expenses.CreateSet(expenses);
			}

			if (businessTrip.Subsistences != null)
			{
				List<Subsistence> subsistences = new List<Subsistence>();
				foreach (SubsistenceDTO subDto in businessTrip.Subsistences)
				{
					Subsistence sub = new Subsistence();
					sub.Trip = trip;
					sub.StartDate = subDto.StartDate;
					sub.DestinationCity = subDto.DestinationCity;
					//sub.CountryId = subDto.CountryId;
					sub.Country = repo.Dictionaries.GetCountry(subDto.CountryId);
					sub.EndDate = subDto.EndDate;
					sub.AccomodationCount = subDto.AccomodationCount;


					List<SubsistenceMeals> meals = new List<SubsistenceMeals>();
					foreach (SubsistenceMealDTO mealDto in subDto.Meals)
					{
						SubsistenceMeals meal = new SubsistenceMeals();
						meal.Type = repo.Dictionaries.GetMealType(mealDto.MealTypeId);
						meal.Subsistence = sub;
						meals.Add(meal);
					}
					sub.Meals = meals;
					subsistences.Add(sub);
				}

				repo.Subsistences.CreateSet(subsistences);
			}

			if (businessTrip.MileageAllowances != null)
			{
				List<MileageAllowance> mileageAllowances = new List<MileageAllowance>();
				foreach (MileageAllowanceDTO maDto in businessTrip.MileageAllowances)
				{
					MileageAllowance ma = new MileageAllowance();
					ma.Trip = trip;
					ma.Date = maDto.Date;
					ma.Distance = maDto.Distance;
					ma.Amount = maDto.Amount;
					ma.Notes = maDto.Notes;
					ma.Type = repo.Dictionaries.GetVehicleType(maDto.VehicleTypeId);

					mileageAllowances.Add(ma);
				}

				repo.MileageAllowances.CreateSet(mileageAllowances);
			}

			this.repo.SaveChanges();
        }

        public IEnumerable<BusinessTripSearchItemDTO> GetForUser(int userId)
        {
            return repo.BusinessTrips.GetForUser(userId);
        }
	}
}
