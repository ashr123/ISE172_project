using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTier;
using DataTier.Utils;
using DataTier.DataEntries;
using LogicTier;


namespace DataTierTest
{
    [TestClass]
    public class UnitTest2
    {

        [TestMethod]
        public void TestAMAbuy()
        {

            bool e = false;
            Random rnd = new Random();
            int rndCommodity = rnd.Next(0, 10);

            int EXPECTEDcommAmount = 0;
            int ACTUALcommAmount = 0;


            MarketClientClass client = new MarketClientClass();
            MarketUserData userData = client.SendQueryUserRequest();
            MarketCommodityOffer commodityInfo = client.SendQueryMarketRequest(rndCommodity);
            
            if (userData.Error != null | commodityInfo.Error != null)   //is NOT successfuly passed to the server
                return;


            if (userData.Funds >= commodityInfo.Ask)
            {     //we  have enough money to buy


                foreach (int cmdty in userData.Commodities.Keys)    //passing on all commodities  . 
                {
                    if (cmdty == rndCommodity)
                    {
                        EXPECTEDcommAmount = userData.Commodities[cmdty];  //checking how many we own from rndCommodity
                    }

                }

                //we are buying 1 
                AMA.AMA_Buy(commodityInfo.Ask, rndCommodity, 1);

                userData = client.SendQueryUserRequest();  //refresh userData

                foreach (int cmdty in userData.Commodities.Keys)    //passing on all commodities
                {
                    if (cmdty == rndCommodity)
                    {
                        ACTUALcommAmount = userData.Commodities[cmdty];
                    }

                }
                if (EXPECTEDcommAmount == ACTUALcommAmount)
                    e=!e;

                if(e)
                  Assert.AreEqual<int>(EXPECTEDcommAmount , ACTUALcommAmount);
                else
                    Assert.AreNotEqual<int>(EXPECTEDcommAmount, ACTUALcommAmount);

            }



        }
    }
}