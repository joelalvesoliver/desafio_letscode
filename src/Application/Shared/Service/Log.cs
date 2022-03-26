using Lets.Code.Application.Shared.Domain.Models;
using System;

namespace Lets.Code.Application.Shared.Service
{
    public static class Log
    {
        public static void LogUpdate(CardModel card)
        {
            var acao = "Atualizado";
            var data = DateTime.Now.ToString("dd/MM/yyyyy mm:HH:ss");
            Print($"{data} - Card {card.Id} - {card.Title} - {acao}");
        }
        public static void LogDelete(CardModel card)
        {
            var acao = "Removido";
            var data = DateTime.Now.ToString("dd/MM/yyyyy mm:HH:ss");
            Print($"{data} - Card {card.Id} - {card.Title} - {acao}");
                
        }

        static void Print(string text)
        {
            Console.WriteLine(text);
        }
    }
}
