﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> 
        where T : class, IEntity, new()

    {
        int Add(T entity);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities); 
        void Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        //asenkronlarini ekle, add int'e cevir
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        Task<IList<T>> GetListAsync(IList<Expression<Func<T, bool>>> predicates, IList<Expression<Func<T, object>>> includePropertries);//filtre ekleyerek getlist islemlerinde birbirinin aynisi metotlar tanimlamayiz. //take al
        Task<List<T>> GetListAsync2(IQueryable<T> query);
        //getdata, datayi sort etmenin yolunu bul.
        T Get(Expression<Func<T, bool>> filter);
        //get by id, get by guid ekle, //imzalari var, bussiness katmanina ekle.
        //dropdown list ekle (async ve normal ekle)
        //datalari page ile de al
        //gelen datalari da sort eden func yaz
        //kullanicidan istenen bilgiyi alabilecegim generic yapi olusturmaya calis
        //
        //get async metotlarina gerek yok, linkit ile predicate atamasi yapabiliriz.
        
        //end
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);//gerek kalmaz
        //search eklenebilir
        //getasqueryable icin arastirma yap, nerelerde nasil kullanabilirim
        //sum ekle, avg ekle
        //direkt sql kodu yazarak sorgu olusturmak icin.
        Task<IList<T>> FromSqlRawAsync(string sql, params object[] parameters);
        //sayfalama icin
        Task<List<T>> GetPagedList<TKey> (int skipCount, int maxResultCount, Expression<Func<T, bool>> predicate= null, Expression<Func<T, TKey>> orderBy=null, bool isAscending=true, params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> DetailsAsync(params Expression<Func<T, object>>[] propertySelectors);
        IQueryable<T> AddDetails(IQueryable<T> query, Expression<Func<T, object>>[] propertySelectors);
        Task<IQueryable<T>> GetQueryableAsync();
        Task<DbSet<T>> GetDbAsync();
        IQueryable<T> GetQueryable(params Expression<Func<T, object>>[] includeProperties);
        //sayfalama icin
        Task<T> GetByIdAsync(int id);

        Task<T> GetByGuid(Guid guid);
        Task<float> SumAsync(Expression<Func<T, bool>> predicate = null);
        Task<float> AvgAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetValueAsync(Expression<Func<T, bool>> predicate);

    }
}
