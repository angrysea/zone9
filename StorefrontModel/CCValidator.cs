using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;

namespace StorefrontModel
{
    public class CardType 
    {
        public CardType(string lengths, string prefixes, bool checkdigit)
        {
            Lengths = lengths;
            Prefixes = prefixes;
            Checkdigit = checkdigit;
        }

        public string Lengths { get; set; }
        public string Prefixes { get; set; }
        public bool Checkdigit { get; set; }
    }

    public class CCValidator
    {
        Dictionary<string, CardType> cards = null;

        public CCValidator()
        {
            cards = new Dictionary<string, CardType>();
            cards["Visa"] = new CardType("13,16", "4", true);
            cards["MasterCard"] = new CardType("16", "51,52,53,54,55", true);
            cards["AmEx"] = new CardType("15", "34,37", true);
            cards["Discover"] = new CardType("16", "6011,650", true);
            cards["DinersClub"] = new CardType("14,16", "300,301,302,303,304,305,36,38,55", true);
            //When we want to support international 
            //cards["CarteBlanche"] = new CardType("14", "300,301,302,303,304,305,36,38", true);
            //cards["JCB"] = new CardType("15,16", "3,1800,2131", true);
            //cards["enRoute"] = new CardType("15", "2014,2149", true);
            //cards["Solo"] = new CardType("16,18,19", "6334, 6767", true);
            //cards["Switch"] = new CardType("16,18,19", "4903,4905,4911,4936,564182,633110,6333,6759", true);
            //cards["Maestro"] = new CardType("16,18", "5020,6", true);
            //cards["VisaElectron"] = new CardType("16", "417500,4917,4913", true);
        }

        private bool CheckDigit(string cardNo)
        {
            int checksum = 0;
            int j = 1;

            for (int i = cardNo.Length - 1; i >= 0; i--)
            {
                int calc = int.Parse(cardNo[i].ToString()) * j;
                if (calc > 9)
                {
                    checksum = checksum + 1;
                    calc = calc - 10;
                }
                checksum = checksum + calc;
                if (j == 1)
                {
                    j = 2;
                }
                else
                {
                    j = 1;
                }
            }

            if (checksum % 10 != 0)
            {
                return false;
            }
            return true;
        }

        public string getCCType(string cardNo)
        {
            string type = null;
            cardNo = Regex.Replace(cardNo, @"[\s-]", "");
            if (cardNo.Length > 0)
            {
                Regex cardexp = new Regex("^[0-9]{13,19}$");
                if (cardexp.IsMatch(cardNo))
                {
                    cardNo = Regex.Replace(cardNo, @"\D", "");
                    foreach (string key in cards.Keys)
                    {
                        CardType cardType = cards[key];
                        string[] lengths = cardType.Lengths.Split(',');
                        for (int i = 0; i < lengths.Length; i++)
                        {
                            if (cardNo.Length == Int32.Parse(lengths[i]))
                            {
                                string[] prefix = cardType.Prefixes.Split(',');
                                for (int j = 0; j < prefix.Length; j++)
                                {
                                    Regex exp = new Regex("^" + prefix[j]);
                                    if (exp.IsMatch(cardNo))
                                    {
                                        if (!cardType.Checkdigit || CheckDigit(cardNo))
                                        {
                                            type = key;
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        if (type != null)
                            break;
                    }
                }
            }
            return type;
        }

        public bool ValidateCC(string cardNo, string cardName)
        {
            bool isValid = false;

            try
            {
                CardType cardType = cards[cardName];
                cardNo = Regex.Replace(cardNo, @"[\s-]", "");
                if (cardNo.Length == 0)
                {
                    return false;
                }

                Regex cardexp = new Regex("^[0-9]{13,19}$");
                if (!cardexp.IsMatch(cardNo))
                {
                    return false;
                }

                cardNo = Regex.Replace(cardNo, @"\D", "");

                if (cardType.Checkdigit)
                {
                    if (!CheckDigit(cardNo))
                        return false;
                }

                string[] prefix = cardType.Prefixes.Split(',');
                for (int i = 0; i < prefix.Length; i++)
                {
                    Regex exp = new Regex("^" + prefix[i]);
                    if (exp.IsMatch(cardNo))
                    {
                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    string[] lengths = cardType.Lengths.Split(',');

                    for (int i = 0; i < lengths.Length; i++)
                    {
                        if (cardNo.Length == Int32.Parse(lengths[i]))
                        {
                            isValid = true;
                            break;
                        }
                    }
                }
            }
            catch (KeyNotFoundException e)
            {
                return false;
            }


            return isValid;
        }
    }
}
