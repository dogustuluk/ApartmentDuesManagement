using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        int Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        //asenkronlarini ekle, add int'e cevir
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        Task<IList<T>> GetListAsync(IList<Expression<Func<T, bool>>> predicates, IList<Expression<Func<T, object>>> includePropertries);//filtre ekleyerek getlist islemlerinde birbirinin aynisi metotlar tanimlamayiz.
        T Get(Expression<Func<T, bool>> filter);
        //get by id, get by guid ekle, //imzalari var, bussiness katmanina ekle.
        //dropdown list ekle (async ve normal ekle)
        //datalari page ile de al
        //gelen datalari da sort eden func yaz
        //kullanicidan istenen bilgiyi alabilecegim generic yapi olusturmaya calis
        //
        
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);//bir filtre uygulanir.
        Task<T> GetAsyncV2(IList<Expression<Func<T, bool>>> predicates, IList<Expression<Func<T, object>>> includeProperties);//birden fazla filtre uygulanir
        Task<T> GetAsyncV3(IList<Expression<Func<T, bool>>> predicates, IList<Expression<Func<T, object>>> includeProperties, Expression<Func<T, int>> selectProperty); //birden fazla filtre uygulanir ama bunlar arasindan tek bir ozellige uyani(select property) uyani getirir. //GetIdByGuid
        Task<T> GetAsyncV4<TResult>(IList<Expression<Func<T, bool>>> predicates, IList<Expression<Func<T,object>>> includeProperties, Expression<Func<T, TResult>> selectProperty);//select property istenilen tipte olabilir, tip guvenligi saglamaz v3'te oldugu gibi.
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        //search eklenebilir
        //getasqueryable icin arastirma yap, nerelerde nasil kullanabilirim

    }
}
