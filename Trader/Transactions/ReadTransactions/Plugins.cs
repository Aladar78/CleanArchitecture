﻿using Infrastructure.Data.App;
using Infrastructure.Data.App.Model;
using Microsoft.EntityFrameworkCore;

namespace Trader.Transactions.ReadTransactions;
public class Plugins {
    public class Repository(AppDbContext db) : Adapters.Repository.IRepository {
        public Task<List<Transaction>> Read(CancellationToken token) =>  db.Transactions.ToListAsync(token);
    }
}