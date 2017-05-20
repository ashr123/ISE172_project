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
    public class UnitTest3

    {

        [TestMethod]
        public void TestAMAsell()
        {


            //dkkdkdkdkdkdk


            Random rnd = new Random();
            int rndCommodity = rnd.Next(0, 10);

            int EXPECTEDcommAmount = 0;
            int ACTUALcommAmount = 0;


            MarketClientClass client = new MarketClientClass();
            MarketUserData userData = client.SendQueryUserRequest();
            MarketCommodityOffer commodityInfo = client.SendQueryMarketRequest(rndCommodity);
            
            if (userData.Error != null | commodityInfo.Error != null)   //is NOT successfuly passed to the server
                return;

            
                foreach (int cmdty in userData.Commodities.Keys)    //passing on all commodities  . 
                {
                    if (cmdty == rndCommodity)
                    {
                        EXPECTEDcommAmount = userData.Commodities[cmdty];  //checking how many we own from rndCommodity
                    }

                }
  

                //we are selling 1 
                AMA.AMA_Sell(commodityInfo.Bid, rndCommodity, 1);

                userData = client.SendQueryUserRequest();  //refresh userData

                foreach (int cmdty in userData.Commodities.Keys)    //passing on all commodities
                {
                    if (cmdty == rndCommodity)
                    {
                        ACTUALcommAmount = userData.Commodities[cmdty];
                    }

                }


            if (EXPECTEDcommAmount == 0)
               Assert.AreEqual<int>(EXPECTEDcommAmount, ACTUALcommAmount);
            else
                Assert.AreEqual<int>(EXPECTEDcommAmount-1, ACTUALcommAmount);




        }
    }
}