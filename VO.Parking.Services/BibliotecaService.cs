using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

//using VO_Biblioteca.LibrarieModele;
//using VO_Biblioteca.Services.Caching;

namespace VO.Parking.Services
{
    //public class BibliotecaService : IBibliotecaService
    //{
    //    private BibliotecaDBContext context;

    //    private ICache cacheManager;

    //    public BibliotecaService(ISettings settings, ICache cacheManager)
    //    {
    //        this.context = new BibliotecaDBContext(settings.DbBibliotecaConnection);
    //        this.cacheManager = cacheManager;
    //    }

    //    public List<Author> GetAllAuthors()
    //    {
    //        if (!cacheManager.IsSet(CacheKeys.AllAuthors))
    //        {
    //            var authors = this.context.Authors.Include(c => c.Country).ToList();
    //            cacheManager.Set(CacheKeys.AllAuthors, authors);
    //        }
    //        return cacheManager.Get<List<Author>>(CacheKeys.AllAuthors);
    //    }

    //    public Author GetAuthor(int id)
    //    {
    //        return this.context.Authors.Find(id);
    //    }

    //    public void AddAuthor(Author author)
    //    {
    //        author.EntryDate = DateTime.Now;
    //        this.context.Authors.Add(author);
    //        this.context.SaveChanges();

    //        cacheManager.Remove(CacheKeys.AllAuthors);
    //    }

    //    public void ChangeAuthor(Author author)
    //    {
    //        // TODO: fix it!!
    //        //author.EntryDate = auth.EntryDate;

    //        var auth = this.GetAuthor(author.AuthorId);
    //        auth.AuthorName = author.AuthorName;
    //        auth.DateOfBirth = author.DateOfBirth;
    //        auth.DateOfDeath = author.DateOfDeath;
    //        auth.IsPopular = author.IsPopular;
    //        auth.CountryId  = author.CountryId;
            
    //        this.context.Entry(auth).State = EntityState.Modified;
    //        this.context.SaveChanges();
    //    }

    //    public List<Country> GetAllCountries()
    //    {
    //        return this.context.Countries.ToList();
    //    }

    //    public List<Genre> GetAllGenres()
    //    {
    //        if (!cacheManager.IsSet(CacheKeys.AllGenres))
    //        {
    //            var genres = this.context.Genres.ToList();
    //            cacheManager.Set(CacheKeys.AllGenres, genres);
    //        }
    //        return cacheManager.Get<List<Genre>>(CacheKeys.AllGenres);
    //    }

    //    public Genre GetGenre(int id)
    //    {
    //        return this.context.Genres.Find(id);

    //    }

    //    public Genre AddGenre(string genreName)
    //    {
    //        var newGenre = new Genre { GenreName = genreName };
    //        this.context.Genres.Add(newGenre);
    //        this.context.SaveChanges();

    //        cacheManager.Remove(CacheKeys.AllGenres);

    //        return newGenre;
    //    }
    //}
}
