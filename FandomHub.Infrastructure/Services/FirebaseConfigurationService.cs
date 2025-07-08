using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FandomHub.Infrastructure.Services
{
	public class FirebaseConfigurationService
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<FirebaseConfigurationService> _logger;

		public FirebaseConfigurationService(IConfiguration configuration, ILogger<FirebaseConfigurationService> logger)
		{
			_configuration = configuration;
			_logger = logger;
		}
		public void InitializeFirebase()
		{
			if (FirebaseApp.DefaultInstance != null)
			{
				_logger.LogInformation("Firebase already initialized");
				return;
			}

			try
			{
				GoogleCredential credential;
				var serviceAccountKey = _configuration["Firebase:ServiceAccountKey"];
				var serviceAccountKeyPath = _configuration["Firebase:ServiceAccountKeyPath"];

				if (!string.IsNullOrEmpty(serviceAccountKey))
				{
					// Cách 1: Từ JSON string trong config
					credential = GoogleCredential.FromJson(serviceAccountKey);
					_logger.LogInformation("Firebase initialized from JSON config");
				}
				else if (!string.IsNullOrEmpty(serviceAccountKeyPath))
				{
					// Cách 2: Từ file path
					if (!File.Exists(serviceAccountKeyPath))
					{
						throw new FileNotFoundException($"Firebase service account file not found: {serviceAccountKeyPath}");
					}

					credential = GoogleCredential.FromFile(serviceAccountKeyPath);
					_logger.LogInformation($"Firebase initialized from file: {serviceAccountKeyPath}");
				}
				else
				{
					throw new InvalidOperationException("Firebase service account configuration not found");
				}

				FirebaseApp.Create(new AppOptions()
				{
					Credential = credential,
					ProjectId = _configuration["Firebase:ProjectId"]
				});

				_logger.LogInformation("Firebase Admin SDK initialized successfully");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to initialize Firebase: {ex.Message}");
				throw;
			}
		}
	
	}
}
