using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TesteDextra.Domain.Entities;
using TesteDextra.Domain.Enum;
using TesteDextra.Infra.Context;
using TesteDextra.Infra.Repository;

namespace ProjectTest
{

    [TestClass]
    public class UnitTest1
    {
        DirectoryInfo currentPath;
        string chromeDriverPath;
        Parametro parametro = new Parametro();
        decimal porcentagemInflacao = 0;
        LancheRepository lancheRepository;
        IWebDriver driver;


        public UnitTest1()
        {
            currentPath = new DirectoryInfo(Environment.CurrentDirectory);
            chromeDriverPath = string.Empty;

            bool found = false;

            while (found != true)
            {
                var auxPath = Directory.GetDirectories(currentPath.FullName);

                for (int i = 0; i < auxPath.Length; i++)
                {
                    if (auxPath[i].Contains("packages"))
                    {
                        found = true;
                        chromeDriverPath = auxPath[i];
                    }
                }

                currentPath = currentPath.Parent;
            }
            ParametroRepository parametroRepository = new ParametroRepository(new TesteDextra.Infra.Context.TesteDextraContext());
            parametro = parametroRepository.GetParametroById((long)ParametroEnum.Inflacao);
            porcentagemInflacao = Convert.ToDecimal(parametro.Valor);
            lancheRepository = new LancheRepository(new TesteDextra.Infra.Context.TesteDextraContext());
        }
        [TestMethod]
        public void ConferirValoresCardapio()
        {
            if (!string.IsNullOrEmpty(chromeDriverPath))
            {

                driver = new ChromeDriver(string.Concat(chromeDriverPath, @"\WebDriver.ChromeDriver.win32.2.34.0\content\"));
                driver.Navigate().GoToUrl("http://localhost:12936/");
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("btnNovoPedido")).Click();

                var ddlLanches = driver.FindElement(By.Id("ddlLanches"));
                var selectElement = new SelectElement(ddlLanches);

                selectElement.SelectByText("X-Bacon");
                CalcularValorXBacon(BuscarValorLancheCardapio());

                selectElement.SelectByText("X-Burger");
                CalcularValorXBurguer(BuscarValorLancheCardapio());


                selectElement.SelectByText("X-Egg");
                CalcularValorXEgg(BuscarValorLancheCardapio());

                selectElement.SelectByText("X-Egg Bacon");
                CalcularValorXEggBacon(BuscarValorLancheCardapio());

                driver.Quit();
            }
        }
        [TestMethod]
        public void ValidarPromocoes()
        {
            var lanches = lancheRepository.GetAllLanches();
            var numeroPromocoes = 3;
            foreach (var item in lanches)
            {
                for (int i = 0; i < numeroPromocoes; i++)
                {
                    if (!string.IsNullOrEmpty(chromeDriverPath))
                    {
                        driver = new ChromeDriver(string.Concat(chromeDriverPath, @"\WebDriver.ChromeDriver.win32.2.34.0\content\"));
                        driver.Navigate().GoToUrl("http://localhost:12936/");
                        driver.Manage().Window.Maximize();
                        driver.FindElement(By.Id("btnNovoPedido")).Click();

                        var ddlLanches = driver.FindElement(By.Id("ddlLanches"));
                        var selectElement = new SelectElement(ddlLanches);
                        selectElement.SelectByText(item.Nome);

                        driver.FindElement(By.Id("btnProximo")).Click();

                        //Promoção Light
                        if (i == 0)
                        {
                            PromocaoLight(item);
                        }//Muita Carne
                        if (i == 1)
                        {
                            PromocaoMuitaCarne(item);
                        }//Muito Queijo
                        if (i == 2)
                        {
                            PromocaoMuitoQueijo(item);
                        }

                        driver.Quit();
                    }
                }
            }

        }
        private void CalcularValorXBacon(decimal valorLancheTela)
        {

            var lanche = lancheRepository.GetLancheByName("X-Bacon");
            decimal valorLanche = 0;
            decimal valorIngrediente;
            foreach (var item in lanche.LancheIngredientes)
            {
                valorIngrediente = 0;
                valorIngrediente = item.Ingrediente.Valor + (item.Ingrediente.Valor * porcentagemInflacao / 100);
                valorLanche += valorIngrediente;
            }

            valorLanche = ArredondarValor(valorLanche);

            if (valorLanche != valorLancheTela)
                throw new Exception("Valor do lanche X-Bacon está errado.");
        }
        private void CalcularValorXBurguer(decimal valorLancheTela)
        {
            var lanche = lancheRepository.GetLancheByName("X-Burger");
            decimal valorLanche = 0;
            decimal valorIngrediente;
            foreach (var item in lanche.LancheIngredientes)
            {
                valorIngrediente = 0;
                valorIngrediente = item.Ingrediente.Valor + (item.Ingrediente.Valor * porcentagemInflacao / 100);
                valorLanche += valorIngrediente;
            }

            valorLanche = ArredondarValor(valorLanche);

            if (valorLanche != valorLancheTela)
                throw new Exception("Valor do lanche X-Burger está errado.");
        }
        private void CalcularValorXEgg(decimal valorLancheTela)
        {
            var lanche = lancheRepository.GetLancheByName("X-Egg");
            decimal valorLanche = 0;
            decimal valorIngrediente;
            foreach (var item in lanche.LancheIngredientes)
            {
                valorIngrediente = 0;
                valorIngrediente = item.Ingrediente.Valor + (item.Ingrediente.Valor * porcentagemInflacao / 100);
                valorLanche += valorIngrediente;
            }

            valorLanche = ArredondarValor(valorLanche);

            if (valorLanche != valorLancheTela)
                throw new Exception("Valor do lanche X-Egg está errado.");

        }
        private void CalcularValorXEggBacon(decimal valorLancheTela)
        {
            var lanche = lancheRepository.GetLancheByName("X-Egg Bacon");
            decimal valorLanche = 0;
            decimal valorIngrediente;
            foreach (var item in lanche.LancheIngredientes)
            {
                valorIngrediente = 0;
                valorIngrediente = item.Ingrediente.Valor + (item.Ingrediente.Valor * porcentagemInflacao / 100);
                valorLanche += valorIngrediente;
            }

            valorLanche = ArredondarValor(valorLanche);

            if (valorLanche != valorLancheTela)
                throw new Exception("Valor do lanche X-Egg Bacon está errado.");

        }
        private decimal BuscarValorLancheCardapio()
        {

            string text = driver.FindElement(By.Id("lblValorLanche")).Text;
            text = text.Replace("R$", "").Replace("Valor Lanche:", "").Trim();
            decimal valorLancheTela = Convert.ToDecimal(text);
            return valorLancheTela;
        }
        private decimal ArredondarValor(decimal valorLanche)
        {
            string valor = String.Format("{0:C}", valorLanche);
            return Convert.ToDecimal(valor.Replace("R$", "").Trim());
        }
        private void PromocaoLight(Lanche lanche)
        {
            var idBotao = String.Concat((int)TesteDextra.Domain.Enum.IngredienteEnum.Alface, "Soma");
            driver.FindElement(By.Id(idBotao)).Click();

            //Se o lanche tiver bacon ou o usuário tiver adicionado como complemento não entra nesta promoção
            var listaIngredientes = this.GetIngredientes(lanche);

            var valor = Convert.ToDecimal(driver.FindElement(By.Id("resultCadastro")).GetAttribute("value"));
            valor = ArredondarValor(valor);
            var valorLancheSemDesc = SomaValorLanche(listaIngredientes);
            if (listaIngredientes.Any(x => x.IdIngrediente == (int)TesteDextra.Domain.Enum.IngredienteEnum.Alface) && !listaIngredientes.Any(x => x.IdIngrediente == (int)TesteDextra.Domain.Enum.IngredienteEnum.Bacon))
            {

                var valorLancheComDesc = valorLancheSemDesc - (valorLancheSemDesc * porcentagemInflacao / 100);

                if (valor != ArredondarValor(valorLancheComDesc))
                {
                    throw new Exception("O valor do lanche esta calculado de forma incorreta.");
                }

                driver.FindElement(By.Id("btnFinalizarPedido")).Click();
            }
            else
            {
                if (valor != ArredondarValor(valorLancheSemDesc))
                {
                    throw new Exception("O valor do lanche esta calculado de forma incorreta.");
                }

                driver.FindElement(By.Id("btnFinalizarPedido")).Click();
            }
        }
        private void PromocaoMuitaCarne(Lanche lanche)
        {
            var idBotao = String.Concat((int)TesteDextra.Domain.Enum.IngredienteEnum.HamburguerCarne, "Soma");
            driver.FindElement(By.Id(idBotao)).Click();

            //Se o lanche tiver bacon ou o usuário tiver adicionado como complemento não entra nesta promoção
            var listaIngredientes = this.GetIngredientes(lanche);

            var valor = Convert.ToDecimal(driver.FindElement(By.Id("resultCadastro")).GetAttribute("value"));
            valor = ArredondarValor(valor);
            var valorLancheSemDesc = SomaValorLanche(listaIngredientes);

            var somaMuitaCarne = listaIngredientes.Count(x => x.IdIngrediente == (int)TesteDextra.Domain.Enum.IngredienteEnum.HamburguerCarne) / 3;

            if (somaMuitaCarne > 0)
            {
                var descontar = listaIngredientes.First(x => x.IdIngrediente == (int)TesteDextra.Domain.Enum.IngredienteEnum.HamburguerCarne).Valor * somaMuitaCarne;

                var valorLancheComDesc = valorLancheSemDesc - descontar;

                if (valor != valorLancheComDesc)
                {
                    throw new Exception("O valor do lanche esta calculado de forma incorreta.");
                }

                driver.FindElement(By.Id("btnFinalizarPedido")).Click();
            }
            else
            {
                if (valor != ArredondarValor(valorLancheSemDesc))
                {
                    throw new Exception("O valor do lanche esta calculado de forma incorreta.");
                }

                driver.FindElement(By.Id("btnFinalizarPedido")).Click();
            }
        }
        private void PromocaoMuitoQueijo(Lanche lanche)
        {
            var idBotao = String.Concat((int)TesteDextra.Domain.Enum.IngredienteEnum.Queijo, "Soma");
            driver.FindElement(By.Id(idBotao)).Click();

            //Se o lanche tiver bacon ou o usuário tiver adicionado como complemento não entra nesta promoção
            var listaIngredientes = this.GetIngredientes(lanche);

            var valor = Convert.ToDecimal(driver.FindElement(By.Id("resultCadastro")).GetAttribute("value"));
            valor = ArredondarValor(valor);
            var valorLancheSemDesc = SomaValorLanche(listaIngredientes);

            var somaMuitoQueijo = listaIngredientes.Count(x => x.IdIngrediente == (int)TesteDextra.Domain.Enum.IngredienteEnum.Queijo) / 3;

            if (somaMuitoQueijo > 0)
            {
                var descontar = listaIngredientes.First(x => x.IdIngrediente == (int)TesteDextra.Domain.Enum.IngredienteEnum.Queijo).Valor * somaMuitoQueijo;

                var valorLancheComDesc = valorLancheSemDesc - descontar;

                if (valor != valorLancheComDesc)
                {
                    throw new Exception("O valor do lanche esta calculado de forma incorreta.");
                }

                driver.FindElement(By.Id("btnFinalizarPedido")).Click();
            }
            else
            {
                if (valor != ArredondarValor(valorLancheSemDesc))
                {
                    throw new Exception("O valor do lanche esta calculado de forma incorreta.");
                }

                driver.FindElement(By.Id("btnFinalizarPedido")).Click();
            }
        }

        private List<Ingrediente> GetIngredientes(Lanche lanche)
        {
            var listIngredientes = new List<Ingrediente>();
            var alfaceQt = Convert.ToInt32(driver.FindElement(By.Id(String.Concat("txtIngrediente", (int)IngredienteEnum.Alface))).GetAttribute("value"));
            var baconQt = Convert.ToInt32(driver.FindElement(By.Id(String.Concat("txtIngrediente", (int)IngredienteEnum.Bacon))).GetAttribute("value"));
            var hamburguerCarneQt = Convert.ToInt32(driver.FindElement(By.Id(String.Concat("txtIngrediente", (int)IngredienteEnum.HamburguerCarne))).GetAttribute("value"));
            var ovoQt = Convert.ToInt32(driver.FindElement(By.Id(String.Concat("txtIngrediente", (int)IngredienteEnum.Ovo))).GetAttribute("value"));
            var queijoQt = Convert.ToInt32(driver.FindElement(By.Id(String.Concat("txtIngrediente", (int)IngredienteEnum.Queijo))).GetAttribute("value"));

            var ingredientes = new IngredienteRepository(new TesteDextraContext()).GetComplementosLanche();
            var ingredienteAlface = ingredientes.First(x => x.IdIngrediente == (int)IngredienteEnum.Alface);
            var ingredienteBacon = ingredientes.First(x => x.IdIngrediente == (int)IngredienteEnum.Bacon);
            var ingredienteHamburguerCarne = ingredientes.First(x => x.IdIngrediente == (int)IngredienteEnum.HamburguerCarne);
            var ingredienteOvo = ingredientes.First(x => x.IdIngrediente == (int)IngredienteEnum.Ovo);
            var ingredientesQueijo = ingredientes.First(x => x.IdIngrediente == (int)IngredienteEnum.Queijo);

            for (var i = 0; i < alfaceQt; i++)
            {
                listIngredientes.Add(ingredienteAlface);
            }

            for (var i = 0; i < baconQt; i++)
            {
                listIngredientes.Add(ingredienteBacon);
            }

            for (var i = 0; i < hamburguerCarneQt; i++)
            {
                listIngredientes.Add(ingredienteHamburguerCarne);
            }

            for (var i = 0; i < ovoQt; i++)
            {
                listIngredientes.Add(ingredienteOvo);
            }

            for (var i = 0; i < queijoQt; i++)
            {
                listIngredientes.Add(ingredientesQueijo);
            }

            listIngredientes.AddRange(lanche.LancheIngredientes.Select(x => x.Ingrediente));

            return listIngredientes;
        }

        private decimal SomaValorLanche(IEnumerable<Ingrediente> listIngredientes)
        {
            return listIngredientes.Sum(item => item.Valor + (item.Valor * porcentagemInflacao / 100));
        }
    }
}
