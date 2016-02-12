﻿using CrazyAppsStudio.Delegacje.Domain.DTO;
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

        public void CreateNewBusinessTrip(BusinessTripDTO businessTrip)
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


					List<SubsistenceMeal> meals = new List<SubsistenceMeal>();
					foreach (SubsistenceMealDTO mealDto in subDto.Meals)
					{
						SubsistenceMeal meal = new SubsistenceMeal();
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

		public void UpdateBusinessTrip(BusinessTripDTO businessTripDto)
		{
			BusinessTrip trip = repo.BusinessTrips.GetById(businessTripDto.Id.Value);

			trip.Title = businessTripDto.Title;
			trip.Date = businessTripDto.Date;
			trip.BusinessReason = businessTripDto.BusinessReason;
			trip.BusinessPurpose = businessTripDto.BusinessPurpose;
			trip.Notes = businessTripDto.Notes;

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
					expense.Date = edto.Date;
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
					trip.Expenses.Add(expense);
				}
			}
		}

		public void UpdateBusinessTripSubsistences(BusinessTrip trip, BusinessTripDTO businessTripDto)
		{
			if (businessTripDto.Expenses != null)
			{
				foreach (Subsistence sub in trip.Subsistences
					.Where(s => businessTripDto.Subsistences
						.Any(sdto => sdto.Id == s.Id)))
				{//Update subsistences that exist both in database and in dto					
					SubsistenceDTO sdto = businessTripDto.Subsistences.First(subsistenceDto => subsistenceDto.Id == sub.Id);
					sub.Trip = trip;
					sub.StartDate = sdto.StartDate;
					sub.DestinationCity = sdto.DestinationCity;
					//sub.CountryId = subDto.CountryId;
					sub.Country = repo.Dictionaries.GetCountry(sdto.CountryId);
					sub.EndDate = sdto.EndDate;
					sub.AccomodationCount = sdto.AccomodationCount;

					repo.Subsistences.RemoveMealsForSubsistence(sub.Id);
					List<SubsistenceMeal> meals = new List<SubsistenceMeal>();
					foreach (SubsistenceMealDTO mealDto in sdto.Meals)
					{
						SubsistenceMeal meal = new SubsistenceMeal();
						meal.Type = repo.Dictionaries.GetMealType(mealDto.MealTypeId);
						meal.Subsistence = sub;
						meals.Add(meal);
					}
					sub.Meals = meals;
				}

				//Remove those that exist in db but don't exist in dto
				repo.Subsistences.RemoveSet(trip.Subsistences
					.Where(s => !businessTripDto.Subsistences
						.Any(sdto => sdto.Id == s.Id)));

				foreach (SubsistenceDTO subDto in businessTripDto.Subsistences.Where(sdto => !trip.Subsistences.Any(s => s.Id == sdto.Id)))
				{//Add those that exist in dto but don't exist in db					
					Subsistence sub = new Subsistence();
					sub.Trip = trip;
					sub.StartDate = subDto.StartDate;
					sub.DestinationCity = subDto.DestinationCity;
					//sub.CountryId = subDto.CountryId;
					sub.Country = repo.Dictionaries.GetCountry(subDto.CountryId);
					sub.EndDate = subDto.EndDate;
					sub.AccomodationCount = subDto.AccomodationCount;

					List<SubsistenceMeal> meals = new List<SubsistenceMeal>();
					foreach (SubsistenceMealDTO mealDto in subDto.Meals)
					{
						SubsistenceMeal meal = new SubsistenceMeal();
						meal.Type = repo.Dictionaries.GetMealType(mealDto.MealTypeId);
						meal.Subsistence = sub;
						meals.Add(meal);
					}
					sub.Meals = meals;
					trip.Subsistences.Add(sub);
				}
			}
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
					allowance.Date = mdto.Date;					
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
					allowance.Date = milDto.Date;					
					allowance.Amount = milDto.Amount;
					allowance.Distance = milDto.Distance;
					allowance.Type = repo.Dictionaries.GetVehicleType(milDto.VehicleTypeId);					
					allowance.Notes = milDto.Notes;
					trip.MileageAllowances.Add(allowance);
				}
			}
		}
	}
}