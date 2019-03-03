using System;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft;
using System.Collections.Generic;
using System.Linq;


namespace Blackjack
{
    public class NewDeckResponse
    {
        [JsonProperty(PropertyName = "success")]
        public Boolean Success { get; set; }

        [JsonProperty(PropertyName = "deck_id")]
        public String DeckID { get; set; }

        [JsonProperty(PropertyName = "shuffled")]
        public Boolean Shuffled { get; set; }

        [JsonProperty(PropertyName = "remaining")]
        public Int16 Remaining { get; set; }
    }

    public class RESTfulCard
    {
        [JsonProperty(PropertyName = "image")]
        public String Image { get; set; }

        [JsonProperty(PropertyName = "value")]
        public String Value { get; set; }

        [JsonProperty(PropertyName = "suit")]
        public String Suit { get; set; }

        [JsonProperty(PropertyName = "code")]
        public String Code { get; set; }
    }

    public class RESTfulDraw
    {
        [JsonProperty(PropertyName = "success")]
        public Boolean Success { get; set; }

        [JsonProperty(PropertyName = "cards")]
        public List<RESTfulCard> Cards { get; set; }

        [JsonProperty(PropertyName = "deck_id")]
        public String DeckID { get; set; }

        [JsonProperty(PropertyName = "remaining")]
        public Int16 Remaining { get; set; }
    }

    class ShoeRESTful: IShoe
    {
        private String DeckID = "zq0gmlvrlb6c";
        private String URL = @"http://deckofcardsapi.com";

        private RestClient Client;
        private Int16 Remaining;

        public ShoeRESTful()
        {
            Client = new RestClient(URL);
            Shuffle();
        }

        private void Shuffle()
        {
            //  >>>>>[  This call creates a new deck, but currently there is an existing deck
            //          I want to re-use. This needs to be updated to re-use an existing deck
            //          or create a new one if the deck_id doesn't exist.
            //          -----
            // var request = new RestRequest("api/deck/new/shuffle/", Method.POST);
            var request = new RestRequest("api/deck/{DeckID}/shuffle/");
            request.AddUrlSegment("DeckID", DeckID);

            var response = Client.Execute<NewDeckResponse>(request);

            if (response.IsSuccessful)
            {
                DeckID = response.Data.DeckID;
                Remaining = response.Data.Remaining;
            }
            else
            {
                throw new Exception("The attempt to communicate with Deck of Cards API was unsuccessful.");
            }
        }

        public String Draw()
        {
            String retval = "";

            var request = new RestRequest("api/deck/{DeckID}/draw/?count=1");
            request.AddUrlSegment("DeckID", DeckID);

            var response = Client.Execute<RESTfulDraw>(request);

            if (response.IsSuccessful)
            {
                switch (response.Data.Cards[0].Value)
                {
                    case "ACE":
                        retval += "A";
                        break;
                    case "JACK":
                        retval += "J";
                        break;
                    case "KING":
                        retval += "Q";
                        break;
                    case "QUEEN":
                        retval += "K";
                        break;
                    case "JOKER":
                        retval += "!";
                        break;
                    default:
                        retval += response.Data.Cards[0].Value;
                        break;

                }

                switch (response.Data.Cards[0].Suit)
                {
                    case "DIAMONDS":
                        retval += "♦";
                        break;
                    case "CLUBS":
                        retval += "♣";
                        break;
                    case "HEARTS":
                        retval += "♥";
                        break;
                    case "SPADES":
                        retval += "♠";
                        break;
                }

                Remaining = response.Data.Remaining;

                if (Empty())
                    Shuffle();
            }
            else
            {
                throw new Exception("The attempt to communicate with Deck of Cards API was unsuccessful.");
            }

            return retval;
        }

        public Boolean Empty()
        {
            return Remaining <= 0;
        }
    }
}
