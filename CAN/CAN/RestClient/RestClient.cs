using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CAN.Models;
using CAN;
using CAN.ViewModels;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace Plugin.RestClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class RestClient<T>
    {
        public const string WebServiceUrl = "http://canapi.promptec.com/";
        public async Task<object> GetAsyncSchool(string url)
        {

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(WebServiceUrl + url);
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
      
        public async Task<bool> UserLogin(LoginData loginData)
        {
            bool Flag = false;
            try
            {
              
                HttpClient client = new HttpClient();
                var byteArray = Encoding.ASCII.GetBytes(loginData.UserName + ":" + loginData.Password);
                //var byteArray = Encoding.ASCII.GetBytes("admin:admin@123");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                HttpResponseMessage response = await client.GetAsync(WebServiceUrl + "api/Auth/Token");
                HttpContent content = response.Content;
                StaticClass.ResponceStatus = "";
                string result = content.ReadAsStringAsync().Result;
                if(result== "Wrong Request")
                {
                    DependencyService.Get<Toast>().Show("Incorrect UserName or Password.");
                }
                if(response.ReasonPhrase =="Not Found")
                {
                    StaticClass.ResponceStatus = "Not Found";
                   
                }
                var LoginData = JsonConvert.DeserializeObject<User>(result);
                if (LoginData != null)
                {
                    if (LoginData.RoleId == 2 || LoginData.RoleId == 4)
                    {

                        var checkUser = App.DAUtil.GetUsers();
                        if (checkUser.Count > 0)
                        {
                            string Token = LoginData.Token;
                            App.DAUtil.updateDataUser(Token);
                        }
                        else
                        {
                            User user = new User();
                            user.UserId = LoginData.UserId;
                            user.UserName = LoginData.UserName;
                            user.Password = StaticClass.UserPassword;
                            user.Name = LoginData.Name;
                            user.Phone = LoginData.Phone;
                            user.Email = LoginData.Email;
                            user.Location = LoginData.Location;
                            user.RoleId = LoginData.RoleId;
                            user.RoleName = LoginData.RoleName;
                            user.Token = LoginData.Token;
                            App.DAUtil.saveDataUser(user);
                        }
                        Flag = true;

                    }
                    else
                    {
                        Flag = false;
                    }
                }
            }
            catch
            {
                Flag = false;
            }
            
           return Flag;
        }

        public async Task<bool> synData(UserLoginById userLoginById)
        {
            
            bool Flag = false;
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + userLoginById.Token);
              //  HttpResponseMessage response1 = await client.GetAsync(WebServiceUrl + "api/Sync/SyncGetEntryLevelDataForAPP?userId="+userLoginById.userId);
               HttpResponseMessage response1 = await client.GetAsync(WebServiceUrl + "api/Sync/SyncGetMasterDataForAPP?userId=" + userLoginById.userId);
                HttpContent content = response1.Content;
                StaticClass.ResponceStatus = "";
                if (response1.ReasonPhrase == "Not Found")
                {
                    StaticClass.ResponceStatus = "Not Found";
                }
                string result = content.ReadAsStringAsync().Result;
                var LoginData = JsonConvert.DeserializeObject<MasterData>(result);
                bool flageDM=true;
               //var checkdataMonth = App.DAUtil.GetDataMonths();
                foreach (var dataMonth in LoginData.dataMonths)
                {
                  if (flageDM == true)
                    {
                        flageDM = false;
                        App.DAUtil.deleteDataMonths();
                    }
                    DataMonths dataMonths = new DataMonths();
                    dataMonths.Datamonthid = dataMonth.datamonthid;
                    dataMonths.Datamonth = dataMonth.datamonth;
                    App.DAUtil.saveDataMonths(dataMonths);
                }
                bool flageCT = true;
                foreach (var CType in LoginData.columnType)
                {
                  if(flageCT == true)
                    {
                      flageCT = false;
                     App.DAUtil.DeleteColumnType();
                    }
                    ColumnType columnType = new ColumnType();
                    columnType.ColumnTypeId = CType.columnTypeId;
                    columnType.TypeName = CType.typeName;
                    App.DAUtil.saveColumnType(columnType);
                }
                bool flageCV = true;
                foreach (var CValue in LoginData.columnValue)
                {
                   if (flageCV==true)
                    {
                       flageCV = false;
                        App.DAUtil.DeleteColumnValues();
                    }

                    ColumnValue columnValue = new ColumnValue();
                    columnValue.columnTypeId = CValue.columnTypeId;
                    columnValue.columnValue = CValue.columnValue;
                    columnValue.columnValueId = CValue.columnValueId;
                    App.DAUtil.saveColumnValue(columnValue);
                }
                bool flageL = true;
                foreach (var loc in LoginData.location)
                {
                    if (flageL==true)
                    {
                        App.DAUtil.DeleteLocation();
                        flageL = false;
                    }
                    Location location = new Location();
                    location.locationId = loc.locationId;
                    location.locationName = loc.locationName;
                    location.parentId = loc.parentId;
                    location.locationTypeId = loc.locationTypeId;
                    location.DCType = loc.DCType;
                    App.DAUtil.saveLocation(location);
                }
                bool flageLm = true;
                foreach (var lmsbmi in LoginData.lmS_BMI)
                {
                   if( flageLm == true)
                    {
                        flageLm = false;
                        App.DAUtil.DeleteLMSBMI();
                    }
                    LMSBMI lmsbi = new LMSBMI();
                    lmsbi.GenderID = lmsbmi.genderId;
                    lmsbi.AgeInDays = lmsbmi.ageInDays;
                    lmsbi.L = lmsbmi.l;
                    lmsbi.M = lmsbmi.m;
                    lmsbi.S = lmsbmi.s;
                    App.DAUtil.saveLMSBMI(lmsbi);
                }
                bool flageLms = true;
                foreach (var lmsha in LoginData.lmS_HAZ)
                {
                   if(flageLms == true)
                    {
                        App.DAUtil.deleteLMSHAZ();
                        flageLms = false;
                    }
                    LMSHAZ lmshaz = new LMSHAZ();
                    lmshaz.GenderID = lmsha.genderId;
                    lmshaz.AgeInDays = lmsha.ageInDays;
                    lmshaz.L = lmsha.l;
                    lmshaz.M = lmsha.m;
                    lmshaz.S = lmsha.s;
                    App.DAUtil.saveLMSHAZ(lmshaz);
                }
                bool flageWaz = true;
                foreach (var lmswa in LoginData.lmS_WAZ)
                {
                    if ( flageWaz == true)
                    {
                        flageWaz = false;
                        App.DAUtil.deleteLMSWAZ();
                    }
                    LMSWAZ lmswaz = new LMSWAZ();
                    lmswaz.GenderID = lmswa.genderId;
                    lmswaz.AgeInDays = lmswa.ageInDays;
                    lmswaz.L = lmswa.l;
                    lmswaz.M = lmswa.m;
                    lmswaz.S = lmswa.s;
                    App.DAUtil.saveLMSWAZ(lmswaz);
                }
                bool flageWFH = true;
                foreach (var lmswaf in LoginData.lmS_WFH)
                {
                    if ( flageWFH == true)
                    {
                        App.DAUtil.deleteLMSWFH();
                        flageWFH = false;
                    }
                    LMSWFH lmswf = new LMSWFH();
                    lmswf.GenderID = lmswaf.genderId;
                    lmswf.HeightInCM = lmswaf.heightInCm;
                    lmswf.L = lmswaf.l;
                    lmswf.M = lmswaf.m;
                    lmswf.S = lmswaf.s;
                    App.DAUtil.saveLMSWFH(lmswf);
                }
                bool flagelf = true;
                foreach (var lmswlf in LoginData.lmS_WFL)
                {
                     if(flagelf == true)
                    {
                        App.DAUtil.deleteLMSWFL();
                        flagelf = false;
                    }
                    LMSWFL lmswfl = new LMSWFL();
                    lmswfl.GenderID = lmswlf.genderId;
                    lmswfl.LengthInCM = lmswlf.lengthInCm;
                    lmswfl.L = lmswlf.l;
                    lmswfl.M = lmswlf.m;
                    lmswfl.S = lmswlf.s;
                    App.DAUtil.saveLMSWFL(lmswfl);
                }
                Flag = true;

            }
            catch(Exception ex)
            {
                return Flag;
            }
            return Flag;
        }
        public async Task<bool> PullNewData(UserLoginById userLoginById)
        {

            bool Flag = false;
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + userLoginById.Token);
                  HttpResponseMessage response1 = await client.GetAsync(WebServiceUrl + "api/Sync/SyncGetEntryLevelDataForAPP?userId="+userLoginById.userId);
               
                HttpContent content = response1.Content;
                string responseBody = content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(responseBody))
                {
                    JObject jobject = JObject.Parse(responseBody);
                    JToken jtoken = jobject.GetValue("familyRegisters");
                    JToken jchildRegisters = jobject.GetValue("childRegisters");
                    JToken jgrowthRegisters = jobject.GetValue("growthRegisters");
                    JToken jredFlagRegisters = jobject.GetValue("redFlagRegisters");
                    JToken jgrowthRegisterMothers = jobject.GetValue("growthRegisterMothers");

                    //string result = content.ReadAsStringAsync().Result;
                    // var LoginPullData = JsonConvert.DeserializeObject<PullData>(result);
                    bool flageDM = true;
                    App.DAUtil.DeleteFamily();
                    foreach (var familyRegisterData in jtoken)
                    {

                        FamilyRegister familyRegister = new FamilyRegister();
                        familyRegister.Assets = familyRegisterData["assets"].ToString(); //familyRegisterData.Assets;
                        familyRegister.CasteTribe = Convert.ToInt32(familyRegisterData["casteTribe"]); //familyRegisterData.CasteTribe;
                                                                                                       // familyRegister.CookingGasAvailable = familyRegisterData.CookingGasAvailable;
                        familyRegister.CreatedBy = Convert.ToInt32(familyRegisterData["createdBy"]);//familyRegisterData.CreatedBy;
                        familyRegister.DOE = Convert.ToDateTime(familyRegisterData["doe"]); //familyRegisterData.DOE;
                        familyRegister.DOU = Convert.ToDateTime(familyRegisterData["dou"]);//familyRegisterData.DOU;
                        familyRegister.ElectricityAvailable = Convert.ToBoolean(familyRegisterData["electricityAvailable"]); //familyRegisterData.ElectricityAvailable;
                        familyRegister.FamilyCode = familyRegisterData["familyCode"].ToString();//familyRegisterData.FamilyCode;
                        familyRegister.FamilyId = Guid.Parse(familyRegisterData["familyId"].ToString());//familyRegisterData.FamilyId;
                        familyRegister.FamilyMigrateToEarn = Convert.ToInt32(familyRegisterData["familyMigrateToEarn"]); //familyRegisterData.FamilyMigrateToEarn;
                        familyRegister.FamilyType = Convert.ToInt32(familyRegisterData["familyType"]);//familyRegisterData.FamilyType;
                        familyRegister.FatherEducation = Convert.ToInt32(familyRegisterData["fatherEducation"]);//familyRegisterData.FatherEducation;
                        familyRegister.FatherName = familyRegisterData["fatherName"].ToString(); //familyRegisterData.FatherName;
                        familyRegister.FatherOccupation = Convert.ToInt32(familyRegisterData["fatherOccupation"]); //familyRegisterData.FatherOccupation;
                        familyRegister.FatherPastDisease = familyRegisterData["fatherPastDisease"].ToString(); //familyRegisterData.FatherPastDisease;
                        familyRegister.HouseType = Convert.ToInt32(familyRegisterData["houseType"]); //familyRegisterData.HouseType;
                        familyRegister.IsMotherWorking = Convert.ToInt32(familyRegisterData["isMotherWorking"]); //familyRegisterData.IsMotherWorking;
                        familyRegister.LocationId = Convert.ToInt32(familyRegisterData["locationId"]); //familyRegisterData.LocationId;
                        familyRegister.MigrationFor = Convert.ToInt32(familyRegisterData["migrationFor"]); //familyRegisterData.MigrationFor;
                        familyRegister.MigrationMonthsPerYear = Convert.ToInt32(familyRegisterData["migrationMonthsPerYear"]);//familyRegisterData.MigrationMonthsPerYear;
                        JToken chekmotherDob = familyRegisterData["motherDob"];
                        if (!string.IsNullOrWhiteSpace(chekmotherDob.ToString()))
                        {
                            familyRegister.MotherDOB = Convert.ToDateTime(familyRegisterData["motherDob"]);  //familyRegisterData.MotherDOB;
                        }

                        familyRegister.MotherEducation = Convert.ToInt32(familyRegisterData["motherEducation"]);//familyRegisterData.MotherEducation;
                        familyRegister.MotherName = familyRegisterData["motherName"].ToString(); //familyRegisterData.MotherName;
                        familyRegister.MotherOccupation = Convert.ToInt32(familyRegisterData["motherOccupation"]);//familyRegisterData.MotherOccupation;
                        familyRegister.MotherPastDisease = familyRegisterData["motherPastDisease"].ToString(); //familyRegisterData.MotherPastDisease;
                        familyRegister.MotherWorkHours = Convert.ToInt32(familyRegisterData["motherWorkHours"]); //familyRegisterData.MotherWorkHours;
                        familyRegister.MotherWorkType = Convert.ToInt32(familyRegisterData["motherWorkType"]);//familyRegisterData.MotherWorkType;
                        familyRegister.NumberofChildenAlive = Convert.ToInt32(familyRegisterData["numberofChildenAlive"]);//familyRegisterData.NumberofChildenAlive;
                        familyRegister.NumberofChildenDied = Convert.ToInt32(familyRegisterData["numberofChildenDied"]); //familyRegisterData.NumberofChildenDied;
                        familyRegister.OwnAgriculturalLand = Convert.ToBoolean(familyRegisterData["ownAgriculturalLand"]);  //familyRegisterData.OwnAgriculturalLand;
                        familyRegister.RationCardType = Convert.ToInt32(familyRegisterData["rationCardType"]); //familyRegisterData.RationCardType;
                        familyRegister.RegisterDate = Convert.ToDateTime(familyRegisterData["registerDate"]);//familyRegisterData.RegisterDate;
                        familyRegister.Religion = Convert.ToInt32(familyRegisterData["religion"]);//familyRegisterData.Religion;
                        familyRegister.ToiletFacility = Convert.ToInt32(familyRegisterData["toiletFacility"]);//familyRegisterData.ToiletFacility;
                        familyRegister.WaterSupplyType = Convert.ToInt32(familyRegisterData["waterSupplyType"]);
                        familyRegister.status = Convert.ToInt32(familyRegisterData["status"]);//familyRegisterData.status;
                        familyRegister.SourceDrinkingWater = Convert.ToInt32(familyRegisterData["sourceDrinkingWater"]); //familyRegisterData.SourceDrinkingWater;
                        familyRegister.CasteTribeName = familyRegisterData["casteTribeName"].ToString(); //familyRegisterData.CasteTribeName;
                        familyRegister.UpdatedBy = Convert.ToInt32(familyRegisterData["updatedBy"]);//familyRegisterData.UpdatedBy;

                        App.DAUtil.SaveFamilyData(familyRegister);

                    }
                    App.DAUtil.DeleteChild();
                    foreach (var childRegisterData in jchildRegisters)
                    {

                        ChildRegister childRegister = new ChildRegister();
                        childRegister.AWCEntryHeightInCms = Convert.ToDecimal(childRegisterData["awcentryHeightInCms"]);//childRegisterData.AWCEntryHeightInCms;
                        childRegister.AWCEntryMUAC = Convert.ToDecimal(childRegisterData["awcentryMuac"]);//childRegisterData.AWCEntryMUAC;
                        childRegister.AWCEntryW4AZ = Convert.ToDecimal(childRegisterData["awcentryW4az"]); //childRegisterData.AWCEntryW4AZ;
                        childRegister.AWCEntryW4HZ = Convert.ToDecimal(childRegisterData["awcentryW4hz"]); //childRegisterData.AWCEntryW4HZ;
                        childRegister.AWCEntryWeightInKG = Convert.ToDecimal(childRegisterData["awcentryWeightInKg"]);  //childRegisterData.AWCEntryWeightInKG;
                        childRegister.BirthDeliveryType = Convert.ToInt32(childRegisterData["birthDeliveryType"]);//childRegisterData.BirthDeliveryType;
                        childRegister.DeliveryTerm = Convert.ToInt32(childRegisterData["deliveryTerm"]);//childRegisterData.DeliveryTerm;
                        childRegister.BirthLengthHeightInCms = Convert.ToDecimal(childRegisterData["birthLengthHeightInCms"]);//childRegisterData.BirthLengthHeightInCms;
                        childRegister.BirthOrder = Convert.ToInt32(childRegisterData["birthOrder"]); //childRegisterData.BirthOrder;
                        childRegister.BirthPlace = Convert.ToInt32(childRegisterData["birthPlace"]);//childRegisterData.BirthPlace;
                        childRegister.BirthWeightInKg = Convert.ToDecimal(childRegisterData["birthWeightInKg"]); //childRegisterData.BirthWeightInKg;
                        childRegister.ChildCode = childRegisterData["childCode"].ToString();//childRegisterData.ChildCode;
                        childRegister.ChildId = Guid.Parse(childRegisterData["childId"].ToString());//childRegisterData.ChildId;
                        childRegister.ChildName = childRegisterData["childName"].ToString(); //childRegisterData.ChildName;
                        childRegister.ChildStatus = Convert.ToInt32(childRegisterData["childStatus"]);//childRegisterData.ChildStatus;
                        childRegister.CreatedBy = Convert.ToInt32(childRegisterData["createdBy"]); //childRegisterData.CreatedBy;
                        childRegister.DOB = Convert.ToDateTime(childRegisterData["dob"]); //childRegisterData.DOB;
                        childRegister.DOE = Convert.ToDateTime(childRegisterData["doe"]);//childRegisterData.DOE;
                        childRegister.DOU = Convert.ToDateTime(childRegisterData["dou"]);//childRegisterData.DOU;
                        childRegister.FamilyId = Guid.Parse(childRegisterData["familyId"].ToString()); //childRegisterData.FamilyId;
                        childRegister.GenderID = Convert.ToInt32(childRegisterData["genderId"]);//childRegisterData.GenderID;
                        string photo = childRegisterData["photograph"].ToString();

                        childRegister.Photograph = Convert.FromBase64String(photo); //childRegisterData.Photograph;
                        childRegister.RegisterDate = Convert.ToDateTime(childRegisterData["registerDate"]); //childRegisterData.RegisterDate;
                        childRegister.UpdatedBy = Convert.ToInt32(childRegisterData["updatedBy"]);//childRegisterData.UpdatedBy;
                        childRegister.GrowthChartGrade = Convert.ToInt32(childRegisterData["growthChartGrade"]); //childRegisterData.GrowthChartGrade;
                        childRegister.GradeOfChild = Convert.ToInt32(childRegisterData["gradeOfChild"]);//childRegisterData.GradeOfChild;
                        childRegister.AnyDisability = childRegisterData["anyDisability"].ToString(); //childRegisterData.AnyDisability;
                        childRegister.AnyIllness = childRegisterData["anyIllness"].ToString(); //childRegisterData.AnyIllness;
                        childRegister.AnyLongTermIllnessInFamily = childRegisterData["anyLongTermIllnessInFamily"].ToString(); //childRegisterData.AnyLongTermIllnessInFamily;
                        JToken AWCDate = childRegisterData["awcEntryDate"];
                        if (!string.IsNullOrWhiteSpace(AWCDate.ToString()))
                            childRegister.AWCEntryDate = Convert.ToDateTime(childRegisterData["awcentryDate"]);//MotherRegister.ExpectedDeliveryDate;

                        App.DAUtil.SaveChildData(childRegister);
                    }
                    App.DAUtil.deleteMother();
                    foreach (var MotherRegister in jgrowthRegisterMothers)
                    {
                        TblGrowthRegisterMother growthRegisterMother = new TblGrowthRegisterMother();
                        // growthRegisterMother.AbdomenalCheckup = MotherRegister.AbdomenalCheckup;
                        //growthRegisterMother.AbnormalHeightLessThan4Ft10Inches = MotherRegister.AbnormalHeightLessThan4Ft10Inches;
                        //growthRegisterMother.AbnormalWeightOrIncreaseInWeight = MotherRegister.AbnormalWeightOrIncreaseInWeight;
                        growthRegisterMother.AncregistraionDate = Convert.ToInt32(MotherRegister["ancregistraionDate"]); //MotherRegister.AncregistraionDate;
                                                                                                                         //growthRegisterMother.AncregistrationMotherWeightInKg = MotherRegister.AncregistrationMotherWeightInKg;
                        growthRegisterMother.AwcregistrationDate = Convert.ToBoolean(MotherRegister["awcRegistrationDate"]); //MotherRegister.AwcregistrationDate;
                                                                                                                             //growthRegisterMother.Bpmeasured = MotherRegister.Bpmeasured;
                                                                                                                             //growthRegisterMother.CheckupDoneInFirstTrimester = MotherRegister.CheckupDoneInFirstTrimester;
                        growthRegisterMother.ChildExpected = Convert.ToBoolean(MotherRegister["childExpected"]);//MotherRegister.ChildExpected;
                        growthRegisterMother.ConsumedIfatablets = Convert.ToBoolean(MotherRegister["consumedIfatablets"]); //MotherRegister.ConsumedIfatablets;
                        growthRegisterMother.CreatedBy = Convert.ToInt32(MotherRegister["createdBy"]);//MotherRegister.CreatedBy;
                                                                                                      //growthRegisterMother.CriticalDisease = MotherRegister.CriticalDisease;
                        growthRegisterMother.DataMonthId = Convert.ToInt32(MotherRegister["dataMonthId"]);// MotherRegister.DataMonthId;
                        growthRegisterMother.DateOfMeasurement = Convert.ToDateTime(MotherRegister["dateOfMeasurement"]); //MotherRegister.DateOfMeasurement;
                        growthRegisterMother.DOE = Convert.ToDateTime(MotherRegister["doe"]); //MotherRegister.DOE;
                        growthRegisterMother.DOU = Convert.ToDateTime(MotherRegister["dou"]);//MotherRegister.DOU;
                                                                                             //growthRegisterMother.EarlierAbortionStillBirthInfacntDeath = MotherRegister.EarlierAbortionStillBirthInfacntDeath;
                                                                                             //growthRegisterMother.EarlierCesarianDelivery = MotherRegister.EarlierCesarianDelivery;
                        growthRegisterMother.EatMealRegularlyFromAwc = Convert.ToInt32(MotherRegister["eatMealRegularlyFromAwc"]); //MotherRegister.EatMealRegularlyFromAwc;
                        growthRegisterMother.EggReceived = Convert.ToInt32(MotherRegister["eggReceived"]); //MotherRegister.EggReceived;
                        JToken chekExpectedDeliveryDate = MotherRegister["expectedDeliveryDate"];
                        if (!string.IsNullOrWhiteSpace(chekExpectedDeliveryDate.ToString()))
                            growthRegisterMother.ExpectedDeliveryDate = Convert.ToDateTime(MotherRegister["expectedDeliveryDate"]);//MotherRegister.ExpectedDeliveryDate;

                        growthRegisterMother.FamilyId = Guid.Parse(MotherRegister["familyId"].ToString());//MotherRegister.FamilyId;
                                                                                                          // growthRegisterMother.FirstDeliveryAfterAge35 = MotherRegister.FirstDeliveryAfterAge35;
                                                                                                          //growthRegisterMother.FrequentVomitAfter4Months = MotherRegister.FrequentVomitAfter4Months;
                        growthRegisterMother.GettingTreatmentFrom = Convert.ToInt32(MotherRegister["gettingTreatmentFrom"]); //MotherRegister.GettingTreatmentFrom;
                        growthRegisterMother.GrowthId = Guid.Parse(MotherRegister["growthId"].ToString()); //MotherRegister.GrowthId;
                                                                                                           //growthRegisterMother.HasDiabetes = MotherRegister.HasDiabetes;
                                                                                                           //growthRegisterMother.Hbmeasured = MotherRegister.Hbmeasured;
                                                                                                           //growthRegisterMother.HeightMeasured = MotherRegister.HeightMeasured;
                                                                                                           // growthRegisterMother.HighBp = MotherRegister.HighBp;
                        growthRegisterMother.IsFirstPregnancy = Convert.ToBoolean(MotherRegister["isFirstPregnancy"]); //MotherRegister.IsFirstPregnancy;
                        growthRegisterMother.IsGettingTreatment = Convert.ToBoolean(MotherRegister["isGettingTreatment"]); //MotherRegister.IsGettingTreatment;
                                                                                                                           // growthRegisterMother.IsHighRisk = MotherRegister.IsHighRisk;
                        growthRegisterMother.IsLactating = Convert.ToBoolean(MotherRegister["isLactating"]); //MotherRegister.IsLactating;
                        growthRegisterMother.IsMealEnough = Convert.ToInt32(MotherRegister["isMealEnough"]); //MotherRegister.IsMealEnough;
                        growthRegisterMother.IsUnderMedication = Convert.ToBoolean(MotherRegister["isUnderMedication"]);//MotherRegister.IsUnderMedication;
                        JToken chekLastDeliveryDate = MotherRegister["lastDeliveryDate"];
                        if (!string.IsNullOrWhiteSpace(chekLastDeliveryDate.ToString()))
                            growthRegisterMother.LastDeliveryDate = Convert.ToDateTime(MotherRegister["lastDeliveryDate"]); //MotherRegister.LastDeliveryDate;

                        //growthRegisterMother.LowHb = MotherRegister.LowHb;
                        growthRegisterMother.MotherWeightIn1Trimester = Convert.ToDecimal(MotherRegister["motherWeightIn1Trimester"]);//MotherRegister.MotherWeightIn1Trimester;
                        growthRegisterMother.MotherWeightIn2Trimester = Convert.ToDecimal(MotherRegister["motherWeightIn2Trimester"]); //MotherRegister.MotherWeightIn2Trimester;
                        growthRegisterMother.MotherWeightInDeliveryTime = Convert.ToDecimal(MotherRegister["motherWeightInDeliveryTime"]); //MotherRegister.MotherWeightInDeliveryTime;
                                                                                                                                           // growthRegisterMother.PreEclampsiaSymptoms = MotherRegister.PreEclampsiaSymptoms;
                                                                                                                                           //growthRegisterMother.PregnancyAfterLongTreatment = MotherRegister.PregnancyAfterLongTreatment;
                        growthRegisterMother.QualityOfAwcfood = Convert.ToInt32(MotherRegister["qualityOfAwcfood"]);//MotherRegister.QualityOfAwcfood;
                        growthRegisterMother.ReceivedCalciumTablets = Convert.ToBoolean(MotherRegister["receivedCalciumTablets"]); //MotherRegister.ReceivedCalciumTablets;
                        growthRegisterMother.ReceivedDietUnderAay = Convert.ToBoolean(MotherRegister["receivedDietUnderAay"]); //MotherRegister.ReceivedDietUnderAay;
                        growthRegisterMother.ReceivedIfatablets = Convert.ToBoolean(MotherRegister["receivedIfatablets"]); //MotherRegister.ReceivedIfatablets;
                        growthRegisterMother.ReceivedMealFromAwcunderAay = Convert.ToBoolean(MotherRegister["receivedMealFromAwcunderAay"]); //MotherRegister.ReceivedMealFromAwcunderAay;
                        JToken chekHowManyReceivedMealFromAwcunderAay = MotherRegister["howManyMonthsReceiveMealsAWCUnderAAY"];
                        if (!string.IsNullOrWhiteSpace(chekHowManyReceivedMealFromAwcunderAay.ToString()))
                            growthRegisterMother.HowManyReceivedMealFromAwcunderAay = Convert.ToInt32(MotherRegister["howManyMonthsReceiveMealsAWCUnderAAY"]);

                        growthRegisterMother.ReceivedSnacksFromAwc = Convert.ToInt32(MotherRegister["receivedSnacksFromAwc"]); //MotherRegister.ReceivedSnacksFromAwc;
                        growthRegisterMother.ReceivedSnacksUnderAay = Convert.ToBoolean(MotherRegister["receivedSnacksUnderAay"]); //MotherRegister.ReceivedSnacksUnderAay;
                        growthRegisterMother.ResonForNotEatingAwcmeal = Convert.ToInt32(MotherRegister["resonForNotEatingAwcmeal"]); //MotherRegister.ResonForNotEatingAwcmeal;
                        growthRegisterMother.GetMealAwcUnderAay = Convert.ToBoolean(MotherRegister["getMealAwcUnderAay"]);

                        //growthRegisterMother.SonographyDone = MotherRegister.SonographyDone;
                        //growthRegisterMother.Ttgiven = MotherRegister.Ttgiven;
                        //growthRegisterMother.TwinsOrFrequentPregnancy = MotherRegister.TwinsOrFrequentPregnancy;
                        growthRegisterMother.TypeOfMeal = Convert.ToInt32(MotherRegister["typeOfMeal"]); //MotherRegister.TypeOfMeal;
                        growthRegisterMother.UpdatedBy = Convert.ToInt32(MotherRegister["updatedBy"]); //MotherRegister.UpdatedBy;
                                                                                                       //growthRegisterMother.WeightInKgDuringRegistration = MotherRegister.WeightInKgDuringRegistration;
                                                                                                       //growthRegisterMother.WeightMeasured = MotherRegister.WeightMeasured;
                        growthRegisterMother.TotalPregnancyMonths = MotherRegister["totalPregnancyMonths"].ToString();
                        growthRegisterMother.HighRiskMotherHistory = MotherRegister["highRiskMotherHistory"].ToString();
                        JToken chekANCCheckups = MotherRegister["ancCheckups"];
                        if (!string.IsNullOrWhiteSpace(chekANCCheckups.ToString()))
                            growthRegisterMother.ANCCheckups = MotherRegister["ancCheckups"].ToString();

                        growthRegisterMother.ANMMarkHighRiskScreening = MotherRegister["anmMarkHighRiskScreening"].ToString();
                        App.DAUtil.SaveGrowthRegisterMother(growthRegisterMother);

                    }
                    App.DAUtil.DeleteGrowthRegisters();
                    foreach (var growthRegistersData in jgrowthRegisters)
                    {
                        GrowthRegister growthRegister = new GrowthRegister();
                        growthRegister.AdmittedToAWC = Convert.ToBoolean(growthRegistersData["admittedToAwc"]);// growthRegistersData.AdmittedToAWC;
                        growthRegister.AgeInDays = Convert.ToInt32(growthRegistersData["ageInDays"]); //growthRegistersData.AgeInDays;
                        growthRegister.AnyDisability = growthRegistersData["anyDisability"].ToString();// growthRegistersData.AnyDisability;
                        growthRegister.AnyIllness = growthRegistersData["anyIllness"].ToString();//growthRegistersData.AnyIllness;
                        growthRegister.AnyRedFlag = Convert.ToBoolean(growthRegistersData["anyRedFlag"]); //growthRegistersData.AnyRedFlag;
                        growthRegister.BMI = Convert.ToDouble(growthRegistersData["bmi"]);//growthRegistersData.BMI;
                        growthRegister.BMIZ = Convert.ToDouble(growthRegistersData["bmiz"]);//growthRegistersData.BMIZ;
                        growthRegister.ChildId = Guid.Parse(growthRegistersData["childId"].ToString());//growthRegistersData.ChildId;
                        growthRegister.CreatedBy = Convert.ToInt32(growthRegistersData["createdBy"]);//growthRegistersData.CreatedBy;
                        growthRegister.CurrentlyAttends = Convert.ToInt32(growthRegistersData["currentlyAttends"]); //growthRegistersData.CurrentlyAttends;
                        growthRegister.DataMonthId = Convert.ToInt32(growthRegistersData["dataMonthId"]); //growthRegistersData.DataMonthId;
                        growthRegister.Doe = Convert.ToDateTime(growthRegistersData["doe"]); //growthRegistersData.Doe;
                        growthRegister.Dou = Convert.ToDateTime(growthRegistersData["dou"]); //growthRegistersData.Dou;
                        growthRegister.GenderID = Convert.ToInt32(growthRegistersData["genderId"]); //growthRegistersData.GenderID;
                        growthRegister.GrowthId = Guid.Parse(growthRegistersData["growthId"].ToString());//growthRegistersData.GrowthId;
                        growthRegister.H4AZ = Convert.ToDouble(growthRegistersData["h4az"]); //growthRegistersData.H4AZ;
                        growthRegister.HealthCheckUpDone = Convert.ToBoolean(growthRegistersData["healthCheckUpDone"]); //growthRegistersData.HealthCheckUpDone;
                        growthRegister.IsBreastfeeding = Convert.ToBoolean(growthRegistersData["isBreastfeeding"]);//growthRegistersData.IsBreastfeeding;
                        growthRegister.IsZScoreRedFlag = Convert.ToBoolean(growthRegistersData["isZscoreRedFlag"]); //growthRegistersData.IsZScoreRedFlag;
                        growthRegister.LengthHeight = Convert.ToDecimal(growthRegistersData["lengthHeight"]); //growthRegistersData.LengthHeight;
                        growthRegister.MeasurementDate = Convert.ToDateTime(growthRegistersData["measurementDate"]);//growthRegistersData.MeasurementDate;
                        growthRegister.MUAC = Convert.ToDecimal(growthRegistersData["muac"]); //growthRegistersData.MUAC;
                        growthRegister.NumberofDaysIll = Convert.ToInt32(growthRegistersData["numberOfDaysIll"]);  //growthRegistersData.NumberofDaysIll;
                        growthRegister.ReasonAnthropometryNotTaken = Convert.ToInt32(growthRegistersData["reasonAnthropometryNotTaken"]); //growthRegistersData.ReasonAnthropometryNotTaken;
                        growthRegister.ReceiveAAY = Convert.ToInt32(growthRegistersData["receiveAay"]);// growthRegistersData.ReceiveAAY;
                        growthRegister.ReceiveCookedFood = Convert.ToBoolean(growthRegistersData["receiveCookedFood"]);// growthRegistersData.ReceiveCookedFood;
                        growthRegister.ReceiveTakeHomeRation = Convert.ToBoolean(growthRegistersData["receiveTakeHomeRation"]); //growthRegistersData.ReceiveTakeHomeRation;
                        growthRegister.Remark = growthRegistersData["remark"].ToString(); //growthRegistersData.Remark;
                        growthRegister.TypeOfIllness = growthRegistersData["typeOfIllness"].ToString(); //growthRegistersData.TypeOfIllness;
                        growthRegister.UpdatedBy = Convert.ToInt32(growthRegistersData["updatedBy"]); //growthRegistersData.UpdatedBy;
                        growthRegister.W4AZ = Convert.ToDouble(growthRegistersData["w4az"]);  //growthRegistersData.W4AZ;
                        growthRegister.W4LHZ = Convert.ToDouble(growthRegistersData["w4lhz"]);//growthRegistersData.W4LHZ;
                        growthRegister.WeightInKg = Convert.ToDecimal(growthRegistersData["weightInKg"]); //growthRegistersData.WeightInKg;
                        growthRegister.ChildIllPreviousMonth = Convert.ToBoolean(growthRegistersData["childIllPreviousMonth"]);//growthRegistersData.ChildIllPreviousMonth;
                        growthRegister.ImmunisationCard = growthRegistersData["immunisationCard"].ToString(); //growthRegistersData.ImmunisationCard;
                        App.DAUtil.SaveGrowthRegister(growthRegister);
                    }
                    App.DAUtil.DeleteRedFlagRegisterRegister();
                    foreach (var redFlagRegistersData in jredFlagRegisters)
                    {
                        RedFlagRegister redFlagRegister = new RedFlagRegister();
                        redFlagRegister.AdvicedButNotAdmittedReason = Convert.ToInt32(redFlagRegistersData["advicedButNotAdmittedReason"]); //redFlagRegistersData.AdvicedButNotAdmittedReason;
                        redFlagRegister.AnyBlockerInLastDiagnose = redFlagRegistersData["anyBlockerInLastDiagnose"].ToString(); //redFlagRegistersData.AnyBlockerInLastDiagnose;
                        JToken chekashaVisitDate = redFlagRegistersData["ashaVisitDate"];
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(chekashaVisitDate.ToString()))
                                redFlagRegister.ASHAVisitDate = Convert.ToDateTime(redFlagRegistersData["ashaVisitDate"]); //redFlagRegistersData.ASHAVisitDate;
                        }
                        catch
                        {

                        }
                        redFlagRegister.AWWVisitWithASHA = Convert.ToBoolean(redFlagRegistersData["awwVisitWithAsha"]); //redFlagRegistersData.AWWVisitWithASHA;
                        redFlagRegister.CaseOfReferral = Convert.ToBoolean(redFlagRegistersData["CaseOfReferral"]); //redFlagRegistersData.CaseOfReferral;
                        redFlagRegister.ChildAdmittedInCTCNRC = Convert.ToBoolean(redFlagRegistersData["childAdmittedInCtcnrc"]); //redFlagRegistersData.ChildAdmittedInCTCNRC;
                        redFlagRegister.ChildGoneToHealthCenter = Convert.ToBoolean(redFlagRegistersData["childGoneToHealthCenter"]); //redFlagRegistersData.ChildGoneToHealthCenter;
                        redFlagRegister.ChildId = Guid.Parse(redFlagRegistersData["childId"].ToString()); //redFlagRegistersData.ChildId;
                        redFlagRegister.ChildSeverelyUnderweightSymptoms = Convert.ToInt32(redFlagRegistersData["childSeverelyUnderWeightSymptoms"]);
                        //redFlagRegistersData.ChildSeverelyUnderweightSymptoms;
                        redFlagRegister.ConsultancyProvidedToFamily = Convert.ToBoolean(redFlagRegistersData["consultancyProvidedToFamily"]);
                        //redFlagRegistersData.ConsultancyProvidedToFamily;
                        redFlagRegister.CreatedBy = Convert.ToInt32(redFlagRegistersData["createdBy"]); //redFlagRegistersData.CreatedBy;
                        redFlagRegister.CurrentStatus = Convert.ToInt32(redFlagRegistersData["currentStatus"]); //redFlagRegistersData.CurrentStatus;
                        redFlagRegister.DateMonthId = Convert.ToInt32(redFlagRegistersData["dateMonthId"]); //redFlagRegistersData.DateMonthId;

                        JToken chekdateofAdmission = redFlagRegistersData["dateofAdmission"];
                        if (!string.IsNullOrWhiteSpace(chekdateofAdmission.ToString()))
                            redFlagRegister.DateofAdmission = Convert.ToDateTime(redFlagRegistersData["dateofAdmission"]);//redFlagRegistersData.DateofAdmission;

                        JToken chekdateOfDischarge = redFlagRegistersData["dateOfDischarge"];
                        if (!string.IsNullOrWhiteSpace(chekdateOfDischarge.ToString()))
                            redFlagRegister.DateOfDischarge = Convert.ToDateTime(redFlagRegistersData["dateOfDischarge"]); //redFlagRegistersData.DateOfDischarge;
                        JToken chekdateOfReferral = redFlagRegistersData["dateOfReferral"];
                        if (!string.IsNullOrWhiteSpace(chekdateOfReferral.ToString()))
                            redFlagRegister.DateOfReferral = Convert.ToDateTime(redFlagRegistersData["dateOfReferral"]); //redFlagRegistersData.DateOfReferral;


                        redFlagRegister.Diagnose = redFlagRegistersData["diagnose"].ToString(); //redFlagRegistersData.Diagnose;
                        redFlagRegister.Doe = Convert.ToDateTime(redFlagRegistersData["doe"]);//redFlagRegistersData.Doe;
                        redFlagRegister.Dou = Convert.ToDateTime(redFlagRegistersData["dou"]);//redFlagRegistersData.Dou;
                        redFlagRegister.FirstFollowUp = Convert.ToBoolean(redFlagRegistersData["firstFollowUp"]); //redFlagRegistersData.FirstFollowUp;
                        redFlagRegister.HygeineMaintained = Convert.ToBoolean(redFlagRegistersData["hygeineMaintained"]); //redFlagRegistersData.HygeineMaintained;
                        redFlagRegister.IsEatingSufficiently = Convert.ToInt32(redFlagRegistersData["isEatingSufficiently"]); //redFlagRegistersData.IsEatingSufficiently;
                        redFlagRegister.LengthHeight = Convert.ToDecimal(redFlagRegistersData["lengthHeight"]); //redFlagRegistersData.LengthHeight;
                        redFlagRegister.MeasurementDate = Convert.ToDateTime(redFlagRegistersData["measurementDate"]); //redFlagRegistersData.MeasurementDate;
                        redFlagRegister.MUAC = Convert.ToDecimal(redFlagRegistersData["muac"]); //redFlagRegistersData.MUAC;
                        redFlagRegister.Observation = redFlagRegistersData["observation"].ToString(); //redFlagRegistersData.Observation;
                        redFlagRegister.RedFlagId = Guid.Parse(redFlagRegistersData["redFlagId"].ToString()); //redFlagRegistersData.RedFlagId;
                        redFlagRegister.ReferradTo = Convert.ToInt32(redFlagRegistersData["referradTo"]);//redFlagRegistersData.ReferradTo;
                        redFlagRegister.ReferredToHealthCenter = Convert.ToInt32(redFlagRegistersData["referredToHealthCenter"]);//redFlagRegistersData.ReferredToHealthCenter;
                        redFlagRegister.SecondFollowUp = Convert.ToBoolean(redFlagRegistersData["secondFollowUp"]); //redFlagRegistersData.SecondFollowUp;
                        redFlagRegister.SpecialDietProvided = Convert.ToBoolean(redFlagRegistersData["specialDietProvided"]); //redFlagRegistersData.SpecialDietProvided;
                        redFlagRegister.ThirdFollowUp = Convert.ToBoolean(redFlagRegistersData["thirdFollowUp"]); //redFlagRegistersData.ThirdFollowUp;
                        redFlagRegister.UpdatedBy = Convert.ToInt32(redFlagRegistersData["updatedBy"]);//redFlagRegistersData.UpdatedBy;
                        redFlagRegister.WasOnMedication = Convert.ToInt32(redFlagRegistersData["wasOnMedication"]); //redFlagRegistersData.WasOnMedication;
                        redFlagRegister.WeightInKg = Convert.ToDecimal(redFlagRegistersData["weightInKg"]); //redFlagRegistersData.WeightInKg;
                        redFlagRegister.FourthFollowUp = Convert.ToBoolean(redFlagRegistersData["fourthFollowUp"]);//redFlagRegistersData.FourthFollowUp;
                        redFlagRegister.IsCurrentlyIll = Convert.ToBoolean(redFlagRegistersData["isCurrentlyIll"]); //redFlagRegistersData.IsCurrentlyIll;
                        redFlagRegister.Remark = redFlagRegistersData["remark"].ToString();// redFlagRegistersData.Remark;
                        App.DAUtil.SaveRedFlagRegister(redFlagRegister);
                        // var ched=App.DAUtil.GetAllRedFlagRegister();
                    }
                    Flag = true;
                }
                else
                {
                    Flag = true;
                }
            }
            catch (Exception ex)
            {
                return Flag = false;
            }
            return Flag;
        }


        public async Task<bool> PushNewData(PushData pushData)
        {

            bool Flag = false;
            try
            {
                HttpClient client = new HttpClient();
                var Userdata = App.DAUtil.GetUsers();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Userdata[0].Token);
                var json = JsonConvert.SerializeObject(pushData);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync(WebServiceUrl + "api/Sync/SyncSaveDataForAPP", httpContent);
                HttpContent content = response.Content;
                StaticClass.ResponceStatus = "";
                if (response.ReasonPhrase == "Not Found")
                {
                    StaticClass.ResponceStatus = "Not Found";
                }
                string result = content.ReadAsStringAsync().Result;
               bool flageDM = Convert.ToBoolean(result);
               Flag = flageDM;

            }
            catch (Exception ex)
            {
                return Flag;
            }
            return Flag;
        }
        public async Task<bool> PutAsync(int id, T t)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(t);
           HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(WebServiceUrl + id, httpContent);

            return result.IsSuccessStatusCode;
        }



        public async Task<bool> DeleteAsync(int id, T t)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(WebServiceUrl + id);

            return response.IsSuccessStatusCode;
        }

    }
}
