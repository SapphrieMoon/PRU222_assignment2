﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Politics",
                    CategoryDescription = "News related to political affairs and government updates",
                    IsActive = true,
                    ParentCategoryId = null
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Sports",
                    CategoryDescription = "Latest updates from the sports industry",
                    IsActive = true,
                    ParentCategoryId = null
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Technology",
                    CategoryDescription = "News on technological advancements and innovations",
                    IsActive = true,
                    ParentCategoryId = null
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Entertainment",
                    CategoryDescription = "Covers movies, music, and celebrity news",
                    IsActive = true,
                    ParentCategoryId = null
                },
                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Elections",
                    CategoryDescription = "News related to elections worldwide",
                    IsActive = true,
                    ParentCategoryId = 1 // Child of Politics
                },
                new Category
                {
                    CategoryId = 6,
                    CategoryName = "Football",
                    CategoryDescription = "Latest football news and match updates",
                    IsActive = true,
                    ParentCategoryId = 2 // Child of Sports
                },
                new Category
                {
                    CategoryId = 7,
                    CategoryName = "AI & Machine Learning",
                    CategoryDescription = "Latest updates on artificial intelligence and machine learning",
                    IsActive = true,
                    ParentCategoryId = 3 // Child of Technology
                },
                new Category
                {
                    CategoryId = 8,
                    CategoryName = "Hollywood",
                    CategoryDescription = "News on Hollywood movies and celebrities",
                    IsActive = true,
                    ParentCategoryId = 4 // Child of Entertainment
                }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagId = 1,
                    TagName = "Breaking News",
                    Note = "Important and urgent news updates."
                },
                new Tag
                {
                    TagId = 2,
                    TagName = "Politics",
                    Note = "Covers political topics and government affairs."
                },
                new Tag
                {
                    TagId = 3,
                    TagName = "Sports",
                    Note = "News about sports, teams, and events."
                },
                new Tag
                {
                    TagId = 4,
                    TagName = "Technology",
                    Note = "Covers tech innovations, AI, and software updates."
                },
                new Tag
                {
                    TagId = 5,
                    TagName = "Entertainment",
                    Note = "Covers movies, music, and celebrity news."
                },
                new Tag
                {
                    TagId = 6,
                    TagName = "Finance",
                    Note = "Covers stock markets, economy, and business news."
                },
                new Tag
                {
                    TagId = 7,
                    TagName = "Health",
                    Note = "Medical news, fitness, and health tips."
                },
                new Tag
                {
                    TagId = 8,
                    TagName = "World",
                    Note = "International news and global events."
                },
                new Tag
                {
                    TagId = 9,
                    TagName = "Science",
                    Note = "Scientific discoveries and space exploration."
                },
                new Tag
                {
                    TagId = 10,
                    TagName = "AI & Machine Learning",
                    Note = "Latest trends in artificial intelligence and ML."
                }
            );
            modelBuilder.Entity<SystemAccount>().HasData(
            new SystemAccount
            {
                AccountId = 1,
                AccountName = "Admin nek",
                AccountEmail = "admin@gmail.com",
                AccountRole = 3, // Assuming 3 represents an admin role
                AccountPassword= "123" // Ideally, use a hashed password
            }
            );

            modelBuilder.Entity<SystemAccount>().HasData(
                new SystemAccount
                {
                    AccountId = 2,
                    AccountName = "Lecture nek",
                    AccountEmail = "lecturer@gmail.com",
                    AccountRole = 2,
                    AccountPassword= "123"
                }
            );
            modelBuilder.Entity<SystemAccount>().HasData(
                new SystemAccount
                {
                    AccountId = 3,
                    AccountName = "Staff nek",
                    AccountEmail = "staff@gmail.com",
                    AccountRole = 1,
                    AccountPassword= "123"
                }
            );
        }

    }
}
