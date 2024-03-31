using ConferenceManager.Data.DTO;
using ConferenceManager.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConferenceManager.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task CreateApplication(ApplicationDto applicationDto)
        {
            if (applicationDto.Author == Guid.Empty || string.IsNullOrEmpty(applicationDto.Name) 
                || string.IsNullOrEmpty(applicationDto.Activity.Activity) || string.IsNullOrEmpty(applicationDto.Outline))
            {
                throw new ArgumentException("Не все обязательные поля заполнены");
            }

            //var existingUnsignedApplication = await _applicationRepository.GetUnsignedApplicationByAuthor(applicationDto.Author);
            //if (existingUnsignedApplication != null)
            //{
            //    throw new InvalidOperationException("У вас уже есть не отправленная заявка");
            //}

            var application = new Application
            {
                Author = applicationDto.Author,
                Activity = (ActivityType)Enum.Parse(typeof(ActivityType), applicationDto.Activity.Activity),
                Name = applicationDto.Name,
                Description = applicationDto.Description,
                Outline = applicationDto.Outline,
                CreatedAt = DateTime.UtcNow.Date,
                SubmittedAt = null
            };

            await _applicationRepository.Create(application);
        }
        public async Task UpdateApplication(Guid id, ApplicationDto applicationDto)
        {
            var existingApplication = await _applicationRepository.GetById(id);
            if (existingApplication == null)
            {
                throw new InvalidOperationException("Заявка не найдена");
            }

            if (string.IsNullOrEmpty(applicationDto.Name) || string.IsNullOrEmpty(applicationDto.Activity.Activity) || string.IsNullOrEmpty(applicationDto.Outline))
            {
                throw new ArgumentException("Не все обязательные поля заполнены");
            }

            if (existingApplication.SubmittedAt != null)
            {
                throw new InvalidOperationException("Нельзя редактировать отправленную заявку");
            }

            existingApplication.Name = applicationDto.Name;
            existingApplication.Activity = (ActivityType)Enum.Parse(typeof(ActivityType), applicationDto.Activity.Activity);
            existingApplication.Description = applicationDto.Description;
            existingApplication.Outline = applicationDto.Outline;

            await _applicationRepository.Update(existingApplication);
        }

        public async Task DeleteApplication(Guid id)
        {
            var existingApplication = await _applicationRepository.GetById(id);
            if (existingApplication == null)
            {
                throw new InvalidOperationException("Заявка не найдена");
            }

            if (existingApplication.SubmittedAt != null)
            {
                throw new InvalidOperationException("Нельзя удалять отправленную заявку");
            }

            await _applicationRepository.Delete(existingApplication);
        }

        public async Task SubmitApplication(Guid id)
        {
            var existingApplication = await _applicationRepository.GetById(id);
            if (existingApplication == null)
            {
                throw new InvalidOperationException("Заявка не найдена");
            }

            if (existingApplication.Author == Guid.Empty || existingApplication.Name == null || existingApplication.Activity == null || existingApplication.Outline == null)
            {
                throw new ArgumentException("Не все обязательные поля заполнены");
            }

            if (existingApplication.SubmittedAt != null)
            {
                throw new InvalidOperationException("Заявка уже отправлена на рассмотрение");
            }

            existingApplication.SubmittedAt = DateTime.UtcNow.Date;

            await _applicationRepository.Update(existingApplication);
        }

        public async Task<IEnumerable<ApplicationDto>> GetApplicationsSubmittedAfter(DateTime submittedAfter)
        {
            var applications = await _applicationRepository.GetApplicationsSubmittedAfter(submittedAfter);

            return applications.Select(a => new ApplicationDto
            {
                Id = a.Id,
                Author = a.Author,
                Activity = new ActivityDto { Activity = a.Activity.ToString(), Dicription = GetEnumDescription(a.Activity) },
                Name = a.Name,
                Description = a.Description,
                Outline = a.Outline,
                CreatedAt = a.CreatedAt,
                SubmittedAt = a.SubmittedAt
            });
        }

        public async Task<IEnumerable<ApplicationDto>> GetUnsubmittedApplicationsOlder(DateTime unsubmittedOlder)
        {
            var applications = await _applicationRepository.GetUnsubmittedApplicationsOlder(unsubmittedOlder);

            return applications.Select(a => new ApplicationDto
            {
                Id = a.Id,
                Author = a.Author,
                Activity = new ActivityDto { Activity = a.Activity.ToString(), Dicription = GetEnumDescription(a.Activity) },
                Name = a.Name,
                Description = a.Description,
                Outline = a.Outline,
                CreatedAt = a.CreatedAt,
                SubmittedAt = a.SubmittedAt
            });
        }

        public async Task<ApplicationDto> GetApplication(Guid id)
        {
            var application = await _applicationRepository.GetById(id);

            if (application == null)
            {
                throw new InvalidOperationException("Заявка не найдена");
            }

            return new ApplicationDto
            {
                Id = application.Id,
                Author = application.Author,
                Activity = new ActivityDto { Activity = application.Activity.ToString(), Dicription = GetEnumDescription(application.Activity) },
                Name = application.Name,
                Description = application.Description,
                Outline = application.Outline,
                CreatedAt = application.CreatedAt,
                SubmittedAt = application.SubmittedAt
            };
        }

        public async Task<IEnumerable<ApplicationDto>> GetUnsignedApplications(Guid userId)
        {
            var unsignedApplications = await _applicationRepository.GetUnsignedApplications(userId);

            return unsignedApplications.Select(a => new ApplicationDto
            {
                Id = a.Id,
                Author = a.Author,
                Activity = new ActivityDto { Activity = a.Activity.ToString(), Dicription = GetEnumDescription(a.Activity) },
                Name = a.Name,
                Description = a.Description,
                Outline = a.Outline,
                CreatedAt = a.CreatedAt,
                SubmittedAt = a.SubmittedAt
            });
        }

        public async Task<IEnumerable<ActivityDto>> GetActivities()
        {
            var activityTypeValues = Enum.GetValues(typeof(ActivityType)).Cast<ActivityType>();

            var activities = new List<ActivityDto>();

            foreach (var activityType in activityTypeValues)
            {
                var description = GetEnumDescription(activityType);
                activities.Add(new ActivityDto { Activity = activityType.ToString(), Dicription = description });
            }

            return activities;
        }

        private string GetEnumDescription(ActivityType value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute == null ? value.ToString() : attribute.Description;
        }
        
    }

}
