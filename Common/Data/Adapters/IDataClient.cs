﻿using System.Linq.Expressions;

namespace Common.Data.Adapters;

public interface IDataClient<T> where T : class {
    Task<T> Add(T dataModel, CancellationToken token);
    Task<T> Remove(T dataModel, CancellationToken token);
    Task<T> Update(T dataModel, CancellationToken token);
    Task<T?> Find(long id, CancellationToken token);
    Task<List<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken token);
    Task<List<T>> Find(CancellationToken token);
}