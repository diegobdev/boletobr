﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BoletoBr.Bancos;
using BoletoBr.Bancos.Cef;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoletoBr.UnitTests.Tests.BancosRetorno
{
    [TestClass]
    public class TestRetornoHsbcCef
    {
        [TestMethod]
        public void TestHeaderArquivoRetornoCnab240BancoCef()
        {
            LeitorRetornoCnab240Cef leitor = new LeitorRetornoCnab240Cef(null);

            string valorTesteRegistro =
                "10400000         2106230130001700000000000000000000001839220301600000000RMEX CONSTRUTORA E INCORPORADOC ECON FEDERAL                          22507201400562700036204000000                    RETORNO-PRODUCAO                  000         ";
            
            var resultado = leitor.ObterHeader(valorTesteRegistro);

            Assert.AreEqual(resultado.NomeDoBeneficiario, "RMEX CONSTRUTORA E INCORPORADO");
        }

        [TestMethod]
        public void TestDetalheArquivoRetornoBancoCef()
        {
            LeitorRetornoCnab400Hsbc leitor = new LeitorRetornoCnab400Hsbc(null);


            // Linha detalhe de outro banco.
            string valorTesteRegistro =
                "19912345678909876123450012345678901  1234567890123456 2       1234567890123456    250720149                   10725072014001060                        25072014       10003991234599                                                               0000000000000100        0950         5000123456789011234567890212345600000000000000000000331                                                                1";
            
            var resultado = leitor.ObterRegistrosDetalhe(valorTesteRegistro);

            Assert.AreEqual(resultado.AgenciaCobradora, 12345);
        }

        [TestMethod]
        public void TestTrailerArquivoRetornoBancoCef()
        {
            LeitorRetornoCnab400Hsbc leitor = new LeitorRetornoCnab400Hsbc(null);

            string dadosTesteTrailer = 
                "9201399                                                                                                                                                                                                                                                                                                                                                                                                        1";

            var resultado = leitor.ObterTrailer(dadosTesteTrailer);

            Assert.AreEqual(resultado.CodigoDeRetorno, 2);
            Assert.AreEqual(resultado.CodigoDoBanco, "399");
            Assert.AreEqual(resultado.CodigoDoRegistro, 9);
            Assert.AreEqual(resultado.CodigoDoServico, "01");
            Assert.AreEqual(resultado.NumeroSequencial, 1);
        }

        public void TestRegistrosArquivoRetornoBancoHsbcCarteiraCnr()
        {
            LeitorRetornoCnab400Hsbc leitor = new LeitorRetornoCnab400Hsbc(null);

            string dadosTesteTrailer =
    "02RETORNO01COBRANÇA CNR   12345001234567890 1 EMPRESA ABCDEFGHIJKLMNOPQRSTUV399HSBC           23071406250BPI1234567890AGENCIA ABCDEFGHIJKL1234                                                                                                                                                                                                                                                      VOLSER000001" +
    "9201399                                                                                                                                                                                                                                                                                                                                                                                                        1";

            var resultado = leitor.ProcessarRetorno();
        }
    }
}

