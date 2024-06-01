using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VanThiel.Domain.Entities;

namespace VanThiel.Application.Repositories;

public class DataGenerator
{
    #region [ Singleton ]
    private static readonly Lazy<DataGenerator> _current = new Lazy<DataGenerator>(() => new DataGenerator(), LazyThreadSafetyMode.PublicationOnly);
    public static DataGenerator Current
    {
        get { return _current.Value; }
    }
    #endregion

    #region [ Faker ]
    public Faker<Product> Product { get; private set; }
    public Random Random { get; private set; } = new Random(123);
    #endregion

    #region [ Ctor ]
    public DataGenerator()
    {
        this.SetProductFaker();
    }
    #endregion

    #region [  ]
    private void SetProductFaker()
    {
        this.Product = new Faker<Product>();
        Product.RuleFor(x => x.Id, Guid.NewGuid().ToString())
                .RuleFor(x => x.CreatedAt, DateTime.Now)
                .RuleFor(x => x.LastUpdatedAt, DateTime.Now)
                .RuleFor(x => x.IsActive, true)
                
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                .RuleFor(x => x.Category, f => f.Lorem.Word())
                .RuleFor(x => x.Price, f => f.Random.Number(99999))
                .RuleFor(x => x.Discount, f => f.Random.Number(30))
                .RuleFor(x => x.Instock, f => f.Random.Number(400))
                .RuleFor(x => x.ImageUrl, f => f.Image.LoremPixelUrl());

        return;
    }

    public IEnumerable<Product> CreateData(int value)
    {
        var result = new List<Product>();

        this.SetProductFaker();
        result = this.Product.Generate(value);

        foreach (var item in result)
        {
            item.Id = Guid.NewGuid().ToString();
        }

        return result;
    }
    #endregion
}
