using System;
using System.Collections.Generic;
using System.Text;
using CAN;
using CAN.Models;
namespace CAN.Helper
{
    public class ZScores
    {
        public int Gender { get; set; }
        public int AgeInDays { get; set; }
        public double WeightInKG { get; set; }
        public double HeightInCM { get; set; }
        public double H4AZ
        {
            get { return this.H4AZ; }
            set
            {
                var d = App.DAUtil.GetLMSHAZ(this.Gender, this.AgeInDays);

                double L = d[0].L, M = d[0].M, S = d[0].S;


                this.H4AZ = Math.Round((Math.Pow((this.HeightInCM / M), L) - 1) / (S * L), 2);
            }
        }
        public double W4AZ
        {
            get { return this.W4AZ; }
            set
            {

                var d = App.DAUtil.GetLMSWAZ(this.Gender, this.AgeInDays);

                double L = d[0].L, M = d[0].M, S = d[0].S;

                //            l_RetVal = (POWER((p_WeightInKG / l_MVal), l_LVal) - 1) / (l_SVal * l_LVal);
                this.W4AZ = Math.Round((Math.Pow((this.WeightInKG / M), L) - 1) / (S * L), 2);
                

                if(this.W4AZ<-3)
                {
                    double l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));// l_MVal * POWER((1 + l_LVal * l_SVal * (-3)), (1 / l_LVal));
                    double l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
                    this.W4AZ= (-3) - ((l_sd3neg - this.WeightInKG) / l_sd23neg);
                }
               
                else if(this.W4AZ>3)
                {
                    double l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
                    double l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
                    this.W4AZ= 3 + ((this.WeightInKG - l_sd3pos) / l_sd23pos);
                }

                //            l_RetVal = ROUND(l_RetVal, 2);



            }
        }
        public double W4LHZ {
            get {
                return this.W4LHZ;
            }
            set
            {
                var d = App.DAUtil.GetLMSWFL(this.Gender, this.HeightInCM);
                double L = d[0].L, M = d[0].M, S = d[0].S, LENHEI2 = d[0].LengthInCM;

                LENHEI2 = Math.Round(LENHEI2, 1);
                this.W4LHZ = Math.Round((Math.Pow((this.WeightInKG / M), L) - 1) / (S * L), 2);
                if(W4LHZ<-3)
                {
                    double l_sd3neg , l_sd23neg;
                    l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));
                    l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
                    this.W4LHZ = (-3) - ((l_sd3neg - WeightInKG) / l_sd23neg);
                }
               else if(W4LHZ >3)
                {
                    double l_sd3pos, l_sd23pos;
                    l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
                    l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
                    this.W4LHZ = 3 + ((WeightInKG - l_sd3pos) / l_sd23pos);
                }
               
                this.W4LHZ = Math.Round(W4LHZ, 2);

            }
        }

        public double BMI {
            get
            {
                return BMI;
            }
            set
            {
                this.BMI = Math.Round((WeightInKG * 10000) / (HeightInCM * HeightInCM), 2);

            }
        }
        public double BMIZ
        {
            get
            {
                return this.BMIZ;
            }
            set
            {
                var d = App.DAUtil.GetLMSBMI(this.Gender, this.AgeInDays);

                double BMI, L = d[0].L, M = d[0].M, S = d[0].S;

                BMI = WeightInKG * 10000 / (HeightInCM * HeightInCM);
                this.BMIZ = (Math.Pow((BMI / M), L) - 1) / (S * L);
                if(BMIZ<-3)
                {
                    double l_sd3neg , l_sd23neg ;
                    l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));
                    l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
                    this.BMIZ = (-3) - ((l_sd3neg - BMI) / l_sd23neg);
                }
               else if(BMIZ>3)
                {
                    double l_sd3pos , l_sd23pos ;
                    l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
                    l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
                    this.BMIZ = 3 + ((BMI - l_sd3pos) / l_sd23pos);
                }
               
                this.BMIZ = Math.Round(this.BMIZ, 2);



            }
        }

        public ZScores(int Gender, int AgeInDays, double WeightInKG, double HeightInCM)
        {
            this.Gender = Gender;
            this.AgeInDays = AgeInDays;
            this.WeightInKG = WeightInKG;
            this.HeightInCM = HeightInCM;


        }
    }
}