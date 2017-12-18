using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TesteDextra.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteDextra.Domain.Entities;

namespace TesteDextra.Infra.Context
{
    public class TesteDextraContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region modelBuilder

            modelBuilder.ApplyConfiguration(new IngredienteMap());
            modelBuilder.ApplyConfiguration(new LancheMap());
            modelBuilder.ApplyConfiguration(new LancheIngredienteMap());
            modelBuilder.ApplyConfiguration(new ParametroMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new PedidoIngredienteMap());
            modelBuilder.ApplyConfiguration(new StatusPedidoMap());
            modelBuilder.ApplyConfiguration(new TipoPromocaoMap());

            #endregion


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        public void EnsureSeedData(TesteDextraContext context)
        {

            Ingrediente alface;
            Ingrediente bacon;
            Ingrediente hamburguerCarne;
            Ingrediente ovo;
            Ingrediente queijo;

            #region Ingrediente

            alface = new Ingrediente
            {
                IdIngrediente = 1,
                Valor = (decimal)0.40,
                Nome = "Alface"
            };
            bacon = new Ingrediente
            {
                IdIngrediente = 2,
                Valor = (decimal)2,
                Nome = "Bacon"
            };
            hamburguerCarne = new Ingrediente
            {
                IdIngrediente = 3,
                Valor = (decimal)3,
                Nome = "Hambúrguer de carne"
            };
            ovo = new Ingrediente
            {
                IdIngrediente = 4,
                Valor = (decimal)0.80,
                Nome = "Ovo"
            };
            queijo = new Ingrediente
            {
                IdIngrediente = 5,
                Valor = (decimal)1.50,
                Nome = "Queijo"
            };

            var listIngredientes = new List<Ingrediente> { alface, bacon, hamburguerCarne, ovo, queijo };

            if ((!context.Set<Ingrediente>().Any()) || (context.Set<Ingrediente>().Any(x => !listIngredientes.Any(y => y.IdIngrediente == x.IdIngrediente))))
            {
                context.AddRange(listIngredientes);
                context.SaveChanges();
            }

            #endregion

            #region Lanche

            Lanche xBacon;
            Lanche xBurger;
            Lanche xEgg;
            Lanche xEggBacon;

            xBacon = new Lanche
            {
                IdLanche = 1,
                Nome = "X-Bacon"
            };
            xBurger = new Lanche
            {
                IdLanche = 2,
                Nome = "X-Burger"
            };
            xEgg = new Lanche
            {
                IdLanche = 3,
                Nome = "X-Egg"
            };
            xEggBacon = new Lanche
            {
                IdLanche = 4,
                Nome = "X-Egg Bacon"
            };

            var listLanche = new List<Lanche> { xBacon, xBurger, xEgg, xEggBacon };

            if ((!context.Set<Lanche>().Any()) || (context.Set<Lanche>().Any(x => !listLanche.Any(y => y.IdLanche == x.IdLanche))))
            {
                context.AddRange(listLanche);
                context.SaveChanges();

                #region LancheIngrediente

                context.Add(new LancheIngrediente
                {
                    IdLanche = xBacon.IdLanche,
                    IdIngrediente = bacon.IdIngrediente
                });

                context.Add(new LancheIngrediente
                {
                    IdLanche = xBacon.IdLanche,
                    IdIngrediente = hamburguerCarne.IdIngrediente
                });
                context.Add(new LancheIngrediente
                {
                    IdLanche = xBacon.IdLanche,
                    IdIngrediente = queijo.IdIngrediente
                });

                context.SaveChanges();


                context.Add(new LancheIngrediente
                {
                    IdLanche = xBurger.IdLanche,
                    IdIngrediente = hamburguerCarne.IdIngrediente
                });
                context.Add(new LancheIngrediente
                {
                    IdLanche = xBurger.IdLanche,
                    IdIngrediente = queijo.IdIngrediente
                });

                context.SaveChanges();

                context.Add(new LancheIngrediente
                {
                    IdLanche = xEgg.IdLanche,
                    IdIngrediente = ovo.IdIngrediente
                });
                context.Add(new LancheIngrediente
                {
                    IdLanche = xEgg.IdLanche,
                    IdIngrediente = hamburguerCarne.IdIngrediente
                });
                context.Add(new LancheIngrediente
                {
                    IdLanche = xEgg.IdLanche,
                    IdIngrediente = queijo.IdIngrediente
                });

                context.SaveChanges();

                context.Add(new LancheIngrediente
                {
                    IdLanche = xEggBacon.IdLanche,
                    IdIngrediente = ovo.IdIngrediente
                });
                context.Add(new LancheIngrediente
                {
                    IdLanche = xEggBacon.IdLanche,
                    IdIngrediente = bacon.IdIngrediente
                });
                context.Add(new LancheIngrediente
                {
                    IdLanche = xEggBacon.IdLanche,
                    IdIngrediente = hamburguerCarne.IdIngrediente
                });
                context.Add(new LancheIngrediente
                {
                    IdLanche = xEggBacon.IdLanche,
                    IdIngrediente = queijo.IdIngrediente
                });

                context.SaveChanges();

                #endregion
            }

            #endregion

            #region StatusPedido

            StatusPedido efetuado = new StatusPedido
            {
                IdStatusPedido = 1,
                Sigla = "Ef",
                Descricao = "Efetuado"
            };

            StatusPedido pronto = new StatusPedido
            {
                IdStatusPedido = 2,
                Sigla = "Pr",
                Descricao = "Pronto"
            };

            StatusPedido entregue = new StatusPedido
            {
                IdStatusPedido = 3,
                Sigla = "En",
                Descricao = "Entregue"
            };


            StatusPedido cancelado = new StatusPedido
            {
                IdStatusPedido = 4,
                Sigla = "Cn",
                Descricao = "Cancelado"
            };


            var listStatusPedido = new List<StatusPedido> { efetuado, pronto, entregue, cancelado };

            if ((!context.Set<StatusPedido>().Any()) || (context.Set<StatusPedido>().Any(x => !listStatusPedido.Any(y => y.IdStatusPedido == x.IdStatusPedido))))
            {
                context.AddRange(listStatusPedido);
                context.SaveChanges();
            }

            Parametro inflacao =  new Parametro
            {
                IdParametro = 1,
                Nome = "Inflacao",
                Valor = 5.ToString()
            };
             
            var listParametros =  new List<Parametro>{ inflacao };

            if ((!context.Set<Parametro>().Any()) || (context.Set<Parametro>().Any(x => !listParametros.Any(y => y.IdParametro == x.IdParametro))))
            {
                context.AddRange(listParametros);
                context.SaveChanges();
            }
            #endregion


        }
    }

    public static class SqlServerModelBuilderExtensions
    {
        public static PropertyBuilder<decimal?> HasPrecision(this PropertyBuilder<decimal?> builder, int precision,
            int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }

        public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> builder, int precision,
            int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }
    }
}