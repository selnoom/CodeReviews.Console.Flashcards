﻿using Flashcards.selnoom.Data;
using Flashcards.selnoom.Menu;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

DatabaseInitializer.InitializeDatabase();

string connectionString = configuration.GetConnectionString("FlashcardDB");

ServiceCollection services = new ServiceCollection();
services.AddTransient<StackRepository>(provider => new StackRepository(connectionString));
services.AddTransient<FlashcardRepository>(provider => new FlashcardRepository(connectionString));
services.AddTransient<StudySessionRepository>(provider => new StudySessionRepository(connectionString));

services.AddTransient<Menu>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

Menu menu = serviceProvider.GetRequiredService<Menu>();
menu.ShowMenu();