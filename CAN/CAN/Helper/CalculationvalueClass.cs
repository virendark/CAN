using CAN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Helper
{
  public  class CalculationvalueClass
    {
        public double W4LHZ;
        public double W4AZ;
        public double H4AZ;
        public double BMI;
        public double BMIZ;
        public double W4LHZValue(int Gender, int AgeInDays, double HeightInCM, double WeightInKG)
        {
            try
            {

                if (AgeInDays < 731)
                {
                    var d = App.DAUtil.GetLMSWFL(Gender, HeightInCM);
                    if (d.Count > 0)
                    {
                        double L = d[0].L, M = d[0].M, S = d[0].S, LENHEI2 = d[0].LengthInCM;

                        LENHEI2 = Math.Round(LENHEI2, 1);
                        this.W4LHZ = Math.Round((Math.Pow((WeightInKG / M), L) - 1) / (S * L), 2);
                        if (W4LHZ < -3)
                        {
                            double l_sd3neg, l_sd23neg;
                            l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));
                            l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
                            this.W4LHZ = (-3) - ((l_sd3neg - WeightInKG) / l_sd23neg);
                        }
                        else if (W4LHZ > 3)
                        {
                            double l_sd3pos, l_sd23pos;
                            l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
                            l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
                            this.W4LHZ = 3 + ((WeightInKG - l_sd3pos) / l_sd23pos);
                        }
                    }
                }

                else
                {
                 var dH = App.DAUtil.GetLMSWFH(Gender, HeightInCM);
                    if (dH.Count>0)
                    {
                        double L = dH[0].L, M = dH[0].M, S = dH[0].S, LENHEI2 = dH[0].HeightInCM;

                        LENHEI2 = Math.Round(LENHEI2, 1);
                        this.W4LHZ = Math.Round((Math.Pow((WeightInKG / M), L) - 1) / (S * L), 2);
                        if (W4LHZ < -3)
                        {
                            double l_sd3neg, l_sd23neg;
                            l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));
                            l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
                            this.W4LHZ = (-3) - ((l_sd3neg - WeightInKG) / l_sd23neg);
                        }
                        else if (W4LHZ > 3)
                        {
                            double l_sd3pos, l_sd23pos;
                            l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
                            l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
                            this.W4LHZ = 3 + ((WeightInKG - l_sd3pos) / l_sd23pos);
                        }
                    }
                }
      
                return W4LHZ = Math.Round(W4LHZ, 2);
            }
            catch
            {
                return W4LHZ = 0;
            }
        }
        public double W4AZValue( int Gender,int AgeInDays,double WeightInKG)
        {
            try
            {
                var d = App.DAUtil.GetLMSWAZ(Gender, AgeInDays);
                if (d.Count > 0)
                {
                    double L = d[0].L, M = d[0].M, S = d[0].S;

                    //            l_RetVal = (POWER((p_WeightInKG / l_MVal), l_LVal) - 1) / (l_SVal * l_LVal);
                    W4AZ = Math.Round((Math.Pow((WeightInKG / M), L) - 1) / (S * L), 2);


                    if (this.W4AZ < -3)
                    {
                        double l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));// l_MVal * POWER((1 + l_LVal * l_SVal * (-3)), (1 / l_LVal));
                        double l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
                        W4AZ = (-3) - ((l_sd3neg - WeightInKG) / l_sd23neg);
                    }

                    else if (this.W4AZ > 3)
                    {
                        double l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
                        double l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
                        W4AZ = 3 + ((WeightInKG - l_sd3pos) / l_sd23pos);
                    }
                }
                return W4AZ;
            }
            catch
            {
                return W4AZ=0;
            }
        }

        public double H4AZValue(int Gender,int AgeInDays,double HeightInCM)
        {
            try
            {
                var d = App.DAUtil.GetLMSHAZ(Gender, AgeInDays);
                if (d.Count > 0)
                {
                    double L = d[0].L, M = d[0].M, S = d[0].S;


                    H4AZ = Math.Round((Math.Pow((HeightInCM / M), L) - 1) / (S * L), 2);
                }
                return H4AZ;
            }
            catch
            {
                return H4AZ = 0;
            }
        }
        public double BMIValue(double WeightInKG,double HeightInCM)
        {
            try
            {
                BMI = Math.Round((WeightInKG * 10000) / (HeightInCM * HeightInCM), 2);
                return BMI;
            }
            catch
            {
                return BMI=0;
            }
        }
        public double BMIZValue(int Gender,int AgeInDays,double WeightInKG,double HeightInCM)
        {
            try
            {
                var d = App.DAUtil.GetLMSBMI(Gender, AgeInDays);
                if (d.Count > 0)
                {
                    double BMI, L = d[0].L, M = d[0].M, S = d[0].S;

                    BMI = WeightInKG * 10000 / (HeightInCM * HeightInCM);
                    BMIZ = (Math.Pow((BMI / M), L) - 1) / (S * L);
                    if (BMIZ < -3)
                    {
                        double l_sd3neg, l_sd23neg;
                        l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));
                        l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
                        BMIZ = (-3) - ((l_sd3neg - BMI) / l_sd23neg);
                    }
                    else if (BMIZ > 3)
                    {
                        double l_sd3pos, l_sd23pos;
                        l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
                        l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
                        BMIZ = 3 + ((BMI - l_sd3pos) / l_sd23pos);
                    }

                    BMIZ = Math.Round(this.BMIZ, 2);
                }
                return BMIZ;
            }
            catch
            {
                return BMIZ=0;
            }
        }
    }
}

