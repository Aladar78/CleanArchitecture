﻿using Common.Business.Model;
using Common.Validation.Business.Model;
using Experts.Trader.FindTransactions.Triggers.Blazor.InputPort;
using Experts.Trader.FindTransactions.UserStory.InputPort;

namespace Experts.Trader.FindTransactions.Triggers.Blazor;

public class Trigger(IUserStory service) : ITrigger {
    public async Task<ViewModel> Execute(string name, string userId, CancellationToken token) {
        var request = new Request {
            Name = name,
            UserId = userId
        };

        token.ThrowIfCancellationRequested();

        var response = await service.Execute(request, token);

        var viewModel = new ViewModel();

        viewModel.Meta = ToMetaViewModel(response.Request);
        viewModel.Errors = response.Errors.Select(ToErrorViewModel).ToList();
        viewModel.Transactions = new();
        viewModel.Transactions.Rows = response.Transactions.Select(ToTranaztionViewModel).ToList();
        viewModel.Transactions.Columns.Add(x => x.Id);
        viewModel.Transactions.Columns.Add(x => x.Name);

        token.ThrowIfCancellationRequested();

        return viewModel;

        static ViewModel.MetaVM ToMetaViewModel(Request businessModel) => new() {
            Id = businessModel.Id,
        };

        static ViewModel.TransactionVM ToTranaztionViewModel(Transaction businessModel) => new() {
            Id = businessModel.Id,
            Name = businessModel.Name
        };

        static ViewModel.ErrorVM ToErrorViewModel(Error businessModel) => new() {
            Name = businessModel.Name,
            Message = businessModel.Message
        };
    }
}
