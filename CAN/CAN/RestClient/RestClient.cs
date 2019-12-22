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
using System.Threading;

namespace Plugin.RestClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class RestClient<T>
    {
        //public const string WebServiceUrl = "http://canapi.promptec.com/";
        public const string WebServiceUrl = "http://prompteccan.azurewebsites.net/";
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
                HttpContent content = (await client.GetAsync(WebServiceUrl + "api/Auth/Token")).Content;//response.Content;
                StaticClass.ResponceStatus = "";
                string result = content.ReadAsStringAsync().Result;
                if (result == "Wrong Request")
                {
                    DependencyService.Get<Toast>().Show("Incorrect UserName or Password.");
                }
                if (response.ReasonPhrase == "Not Found")
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
                HttpClient client = new HttpClient
                {
                    Timeout = TimeSpan.FromMinutes(20)
                };
                //HttpClient client = new HttpClient();
                //client.Timeout = TimeSpan.FromSeconds(300);
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
                bool flageDM = true;
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
                    if (flageCT == true)
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
                    if (flageCV == true)
                    {
                        flageCV = false;
                        App.DAUtil.DeleteColumnValues();
                    }

                    ColumnValue columnValue = new ColumnValue();
                    columnValue.columnTypeId = CValue.columnTypeId;
                    columnValue.columnValue = CValue.columnValue;
                    columnValue.columnValueId = CValue.columnValueId;
                    columnValue.columnValueSecondary = CValue.columnValueSecondary;
                    columnValue.sequenceNo = CValue.sequenceNo;
                    App.DAUtil.saveColumnValue(columnValue);
                }
                bool flageL = true;
                foreach (var loc in LoginData.location)
                {
                    if (flageL == true)
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
                    if (flageLm == true)
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
                    if (flageLms == true)
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
                    if (flageWaz == true)
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
                    if (flageWFH == true)
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
                    if (flagelf == true)
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
            catch (Exception ex)
            {
                App.DAUtil.DeleteUser();
                return Flag = false;
            }
            return Flag;
        }
        public async Task<bool> PullNewData(UserLoginById userLoginById)
        {
            string responseBody = "";
            bool Flag = true;
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + userLoginById.Token);
                HttpResponseMessage response1 = await client.GetAsync(WebServiceUrl + "api/Sync/SyncGetEntryLevelFamilyDataForAPP?userId=" + userLoginById.userId);
                HttpContent content = response1.Content;
                responseBody = content.ReadAsStringAsync().Result;
                var familyData = JsonConvert.DeserializeObject<RootObject>(responseBody);
                if (!string.IsNullOrEmpty(responseBody))
                {

                    bool flageDM = true;
                    App.DAUtil.DeleteFamily();
                    foreach (var familyRegisterData in familyData.familyRegisters)
                    {

                        FamilyRegister familyRegister = new FamilyRegister();
                        familyRegister.Assets = familyRegisterData.assets != null ? familyRegisterData.assets.ToString() : null;
                        familyRegister.CasteTribe = familyRegisterData.casteTribe.HasValue ? Convert.ToInt32(familyRegisterData.casteTribe) : (int?)null;
                        familyRegister.CreatedBy = Convert.ToInt32(familyRegisterData.createdBy);
                        familyRegister.DOE = Convert.ToDateTime(familyRegisterData.doe);
                        familyRegister.DOU = Convert.ToDateTime(familyRegisterData.dou);
                        familyRegister.ElectricityAvailable = Convert.ToBoolean(familyRegisterData.electricityAvailable);
                        // familyRegister.FamilyCode = familyRegisterData["familyCode"].ToString();
                        familyRegister.NewFamilyCode = Convert.ToInt32(familyRegisterData.newFamilyCode);
                        familyRegister.FamilyId = Guid.Parse(familyRegisterData.familyId.ToString());
                        familyRegister.FamilyMigrateToEarn = familyRegisterData.familyMigrateToEarn.HasValue ? Convert.ToInt32(familyRegisterData.familyMigrateToEarn) : (int?)null;
                        familyRegister.FamilyType = familyRegisterData.familyType.HasValue ? Convert.ToInt32(familyRegisterData.familyType) : (int?)null;
                        familyRegister.FatherEducation = familyRegisterData.fatherEducation.HasValue ? Convert.ToInt32(familyRegisterData.fatherEducation) : (int?)null;
                        familyRegister.FatherName = familyRegisterData.fatherName!= null? familyRegisterData.fatherName.ToString():null;
                        familyRegister.FatherOccupation = familyRegisterData.fatherOccupation!=null? familyRegisterData.fatherOccupation.ToString():null;
                        familyRegister.FatherPastDisease = familyRegisterData.fatherPastDisease!=null? familyRegisterData.fatherPastDisease.ToString():null;
                        familyRegister.HouseType = familyRegisterData.houseType.HasValue ? Convert.ToInt32(familyRegisterData.houseType) : (int?)null;
                        familyRegister.IsMotherWorking = familyRegisterData.isMotherWorking.HasValue ? Convert.ToInt32(familyRegisterData.isMotherWorking) : (int?)null;
                        familyRegister.LocationId = Convert.ToInt32(familyRegisterData.locationId);
                        familyRegister.MigrationFor = familyRegisterData.migrationFor.HasValue ? Convert.ToInt32(familyRegisterData.migrationFor) : (int?)null;
                        familyRegister.MigrationMonthsPerYear = familyRegisterData.migrationMonthsPerYear.HasValue ? Convert.ToInt32(familyRegisterData.migrationMonthsPerYear) : (int?)null;
                        if (familyRegisterData.motherDob.HasValue)
                        {
                            familyRegister.MotherDOB = Convert.ToDateTime(familyRegisterData.motherDob);
                        }
                        else
                        {
                            familyRegister.MotherDOB = null;
                        }
                        familyRegister.MotherEducation = familyRegisterData.motherEducation.HasValue ? Convert.ToInt32(familyRegisterData.motherEducation) : (int?)null;
                        familyRegister.MotherName = familyRegisterData.motherName!=null? familyRegisterData.motherName.ToString():null;
                        familyRegister.MotherOccupation = familyRegisterData.motherOccupation!=null? familyRegisterData.motherOccupation.ToString():null;
                        familyRegister.MotherPastDisease = familyRegisterData.motherPastDisease!=null? familyRegisterData.motherPastDisease.ToString():null;
                        familyRegister.MotherWorkHours = familyRegisterData.motherWorkHours.HasValue ? Convert.ToInt32(familyRegisterData.motherWorkHours) : (int?)null;
                        familyRegister.MotherWorkType = familyRegisterData.motherWorkType.HasValue ? Convert.ToInt32(familyRegisterData.motherWorkType) : (int?)null;
                        familyRegister.NumberofChildenAlive = Convert.ToInt32(familyRegisterData.numberofChildenAlive);
                        familyRegister.NumberofChildenDied = familyRegisterData.numberofChildenDied.HasValue ? Convert.ToInt32(familyRegisterData.numberofChildenDied) : (int?)null;
                        familyRegister.OwnAgriculturalLand = Convert.ToBoolean(familyRegisterData.ownAgriculturalLand);
                        familyRegister.RationCardType = familyRegisterData.rationCardType.HasValue ? Convert.ToInt32(familyRegisterData.rationCardType) : (int?)null;
                        familyRegister.RegisterDate = Convert.ToDateTime(familyRegisterData.registerDate);
                        familyRegister.Religion = familyRegisterData.religion.HasValue ? Convert.ToInt32(familyRegisterData.religion) : (int?)null;
                        familyRegister.ToiletFacility = familyRegisterData.toiletFacility.HasValue ? Convert.ToInt32(familyRegisterData.toiletFacility) : (int?)null;
                        familyRegister.WaterSupplyType = familyRegisterData.waterSupplyType.HasValue ? Convert.ToInt32(familyRegisterData.waterSupplyType) : (int?)null;
                        familyRegister.status = Convert.ToInt32(familyRegisterData.status);
                        familyRegister.SourceDrinkingWater = familyRegisterData.sourceDrinkingWater.HasValue ? Convert.ToInt32(familyRegisterData.sourceDrinkingWater) : (int?)null; familyRegister.CasteTribeName = familyRegisterData.casteTribeName!=null? familyRegisterData.casteTribeName.ToString():null;
                        familyRegister.UpdatedBy = Convert.ToInt32(familyRegisterData.updatedBy);
                        App.DAUtil.SaveFamilyData(familyRegister);

                    }
                    Flag = true;
                }
                HttpClient clientC = new HttpClient();
                responseBody = "";
                HttpResponseMessage responseC = await clientC.GetAsync(WebServiceUrl + "api/Sync/SyncGetEntryLevelChildDataForAPP?userId=" + userLoginById.userId);
                HttpContent contentC = responseC.Content;
                responseBody = contentC.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(responseBody))
                {
                    var ChildData = JsonConvert.DeserializeObject<RootObject>(responseBody);
                    App.DAUtil.DeleteChild();
                    foreach (var chid in ChildData.childRegisters)
                    {
                        ChildRegister childRegister = new ChildRegister();
                        childRegister.AWCEntryHeightInCms = chid.awcentryHeightInCms.HasValue ? Convert.ToDecimal(chid.awcentryHeightInCms) : (decimal?)null;
                        childRegister.AWCEntryMUAC = chid.awcentryMuac.HasValue ? Convert.ToDecimal(chid.awcentryMuac.HasValue) : (decimal?)null;
                        childRegister.AWCEntryW4AZ = chid.awcentryW4az.HasValue ? Convert.ToDecimal(chid.awcentryW4az) : (decimal?)null;
                        childRegister.AWCEntryW4HZ = chid.awcentryW4hz.HasValue ? Convert.ToDecimal(chid.awcentryW4hz) : (decimal?)null;
                        childRegister.AWCEntryWeightInKG = chid.awcentryWeightInKg.HasValue ? Convert.ToDecimal(chid.awcentryWeightInKg) : (decimal?)null;
                        childRegister.BirthDeliveryType = chid.birthDeliveryType.HasValue ? Convert.ToInt32(chid.birthDeliveryType) : (int?)null;
                        childRegister.DeliveryTerm = chid.deliveryTerm.HasValue ? Convert.ToInt32(chid.deliveryTerm) : (int?)null;
                        childRegister.BirthLengthHeightInCms = chid.birthLengthHeightInCms.HasValue ? Convert.ToDecimal(chid.birthLengthHeightInCms) : (decimal?)null;
                        childRegister.BirthOrder = chid.birthOrder.HasValue ? Convert.ToInt32(chid.birthOrder) : (int?)null;
                        childRegister.BirthPlace = chid.birthPlace.HasValue ? Convert.ToInt32(chid.birthPlace) : (int?)null;
                        childRegister.BirthWeightInKg = chid.birthWeightInKg.HasValue ? Convert.ToDecimal(chid.birthWeightInKg) : (decimal?)null;
                        //  childRegister.ChildCode = chid.childCode.ToString();
                        childRegister.ChildId = Guid.Parse(chid.childId.ToString());
                        childRegister.ChildName = chid.childName!=null? chid.childName.ToString():null;
                        childRegister.ChildStatus = Convert.ToInt32(chid.childStatus);
                        childRegister.CreatedBy = Convert.ToInt32(chid.createdBy);
                        childRegister.DOB = Convert.ToDateTime(chid.dob);
                        childRegister.DOE = Convert.ToDateTime(chid.doe);
                        childRegister.DOU = Convert.ToDateTime(chid.dou);
                        childRegister.FamilyId = Guid.Parse(chid.familyId.ToString());
                        childRegister.GenderID = Convert.ToInt32(chid.genderId);
                        if (chid.photograph != null)
                        {
                            string photo = chid.photograph.ToString();
                            childRegister.Photograph = Convert.FromBase64String(photo);
                        }
                        childRegister.RegisterDate = Convert.ToDateTime(chid.registerDate);
                        childRegister.UpdatedBy = Convert.ToInt32(chid.updatedBy);
                        childRegister.GrowthChartGrade = chid.growthChartGrade.HasValue ? Convert.ToInt32(chid.growthChartGrade) : (int?)null;
                        childRegister.GradeOfChild = chid.gradeOfChild.HasValue ? Convert.ToInt32(chid.gradeOfChild) : (int?)null;
                        childRegister.AnyDisability = chid.anyDisability!=null?chid.anyDisability.ToString():null;
                        childRegister.AnyIllness = chid.anyIllness!=null? chid.anyIllness.ToString():null;
                        childRegister.AnyLongTermIllnessInFamily = chid.anyLongTermIllnessInFamily!=null? chid.anyLongTermIllnessInFamily.ToString():null;
                        if (chid.awcEntryDate.HasValue)
                        {
                            StaticClass.AwcDate = Convert.ToDateTime(chid.awcEntryDate);
                            childRegister.AWCEntryDate = StaticClass.AwcDate.Date;//Convert.ToDateTime(childRegisterData["awcentryDate"]);//MotherRegister.ExpectedDeliveryDate;
                        }
                        else
                        {
                            childRegister.AWCEntryDate = null;
                        }
                        childRegister.NewChildCode = Convert.ToInt32(chid.newChildCode);
                        App.DAUtil.SaveChildData(childRegister);
                        

                    }
                    Flag = true;
                }


                HttpClient clientM = new HttpClient();
                responseBody = "";
                HttpResponseMessage responseM = await clientM.GetAsync(WebServiceUrl + "api/Sync/SyncGetEntryLevelMotherDataForAPP?userId=" + userLoginById.userId);

                HttpContent contentM = responseM.Content;
                responseBody = contentM.ReadAsStringAsync().Result;
                
                if (!string.IsNullOrEmpty(responseBody))
                {
                    var growthRegisterMothers = JsonConvert.DeserializeObject<RootObject>(responseBody);
                    App.DAUtil.deleteMother();
                    foreach (var MotherRegister in growthRegisterMothers.growthRegisterMothers)
                    {
                        TblGrowthRegisterMother growthRegisterMother = new TblGrowthRegisterMother();
                        growthRegisterMother.AncregistraionDate = Convert.ToInt32(MotherRegister.ancregistraionDate); 
                        growthRegisterMother.AwcregistrationDate = Convert.ToBoolean(MotherRegister.awcregistrationDate); 
                        growthRegisterMother.ChildExpected = Convert.ToBoolean(MotherRegister.childExpected);
                        growthRegisterMother.ConsumedIfatablets = Convert.ToBoolean(MotherRegister.consumedIfatablets);
                        growthRegisterMother.CreatedBy = Convert.ToInt32(MotherRegister.createdBy);
                        growthRegisterMother.DataMonthId = Convert.ToInt32(MotherRegister.dataMonthId);
                        growthRegisterMother.DateOfMeasurement = Convert.ToDateTime(MotherRegister.dateOfMeasurement);
                        growthRegisterMother.DOE = Convert.ToDateTime(MotherRegister.doe);
                        growthRegisterMother.DOU = Convert.ToDateTime(MotherRegister.dou);
                        growthRegisterMother.EatMealRegularlyFromAwc = Convert.ToInt32(MotherRegister.eatMealRegularlyFromAwc); 
                        growthRegisterMother.EggReceived = Convert.ToInt32(MotherRegister.eggReceived);
                       if (growthRegisterMother.ExpectedDeliveryDate.HasValue)
                        {
                           growthRegisterMother.ExpectedDeliveryDate = Convert.ToDateTime(MotherRegister.expectedDeliveryDate);
                        }
                        growthRegisterMother.FamilyId = Guid.Parse(MotherRegister.familyId.ToString());
                                                                     
                        growthRegisterMother.GettingTreatmentFrom = Convert.ToInt32(MotherRegister.gettingTreatmentFrom);
                        growthRegisterMother.GrowthId = Guid.Parse(MotherRegister.growthId.ToString()); 
                        
                        growthRegisterMother.IsFirstPregnancy = Convert.ToBoolean(MotherRegister.isFirstPregnancy); 
                        growthRegisterMother.IsGettingTreatment = Convert.ToBoolean(MotherRegister.isGettingTreatment); 
                        growthRegisterMother.IsLactating = Convert.ToBoolean(MotherRegister.isLactating);
                        growthRegisterMother.IsMealEnough = Convert.ToInt32(MotherRegister.isMealEnough);
                        growthRegisterMother.IsUnderMedication = Convert.ToBoolean(MotherRegister.isUnderMedication);
                        
                        if (MotherRegister.lastDeliveryDate.HasValue)
                        {
                         
                            growthRegisterMother.LastDeliveryDate = Convert.ToDateTime(MotherRegister.lastDeliveryDate); 
                        }
                      
                        growthRegisterMother.MotherWeightIn1Trimester = Convert.ToDecimal(MotherRegister.motherWeightIn1Trimester);
                        growthRegisterMother.MotherWeightIn2Trimester = Convert.ToDecimal(MotherRegister.motherWeightIn2Trimester); 
                        growthRegisterMother.MotherWeightInDeliveryTime = Convert.ToDecimal(MotherRegister.motherWeightInDeliveryTime);
                        growthRegisterMother.QualityOfAwcfood = Convert.ToInt32(MotherRegister.qualityOfAwcfood);
                        growthRegisterMother.ReceivedCalciumTablets = Convert.ToBoolean(MotherRegister.receivedCalciumTablets);
                        growthRegisterMother.ReceivedDietUnderAay = Convert.ToBoolean(MotherRegister.receivedDietUnderAay); 
                        growthRegisterMother.ReceivedIfatablets = Convert.ToBoolean(MotherRegister.receivedIfatablets); 
                        growthRegisterMother.ReceivedMealFromAwcunderAay = Convert.ToBoolean(MotherRegister.receivedMealFromAwcunderAay);
                      
                            growthRegisterMother.HowManyReceivedMealFromAwcunderAay = MotherRegister.howManyMonthsReceiveMealsAWCUnderAAY.HasValue? Convert.ToInt32(MotherRegister.howManyMonthsReceiveMealsAWCUnderAAY):0;

                        growthRegisterMother.ReceivedSnacksFromAwc = Convert.ToInt32(MotherRegister.receivedSnacksFromAwc);
                        growthRegisterMother.ReceivedSnacksUnderAay = Convert.ToBoolean(MotherRegister.receivedSnacksUnderAay);
                        growthRegisterMother.ResonForNotEatingAwcmeal = Convert.ToInt32(MotherRegister.resonForNotEatingAwcmeal); 
                       // growthRegisterMother.GetMealAwcUnderAay = Convert.ToBoolean(MotherRegister["getMealAwcUnderAay"]);

                       
                        growthRegisterMother.TypeOfMeal = Convert.ToInt32(MotherRegister.typeOfMeal);
                        growthRegisterMother.UpdatedBy = Convert.ToInt32(MotherRegister.updatedBy); 
                                                                                                     
                        growthRegisterMother.TotalPregnancyMonths = MotherRegister.totalPregnancyMonths!=null? MotherRegister.totalPregnancyMonths.ToString():null;
                        growthRegisterMother.HighRiskMotherHistory = MotherRegister.highRiskMotherHistory!=null? MotherRegister.highRiskMotherHistory.ToString():null;
                        
                            growthRegisterMother.ANCCheckups = MotherRegister.ancCheckups!=null? MotherRegister.ancCheckups.ToString():null;

                        growthRegisterMother.ANMMarkHighRiskScreening = MotherRegister.anmMarkHighRiskScreening!=null? MotherRegister.anmMarkHighRiskScreening.ToString():null;
                        App.DAUtil.SaveGrowthRegisterMother(growthRegisterMother);

                    }
                }
                HttpClient clientG = new HttpClient();
                responseBody = "";
                HttpResponseMessage responseG = await clientG.GetAsync(WebServiceUrl + "api/Sync/SyncGetEntryLevelChildGrowthDataForAPP?userId=" + userLoginById.userId);

                HttpContent contentG = responseG.Content;
                responseBody = contentG.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(responseBody))
                {
                    var growthRegisters = JsonConvert.DeserializeObject<RootObject>(responseBody);
                    App.DAUtil.DeleteGrowthRegisters();
                    foreach (var growthRegistersData in growthRegisters.growthRegisters)
                    {
                        GrowthRegister growthRegister = new GrowthRegister();
                        growthRegister.AdmittedToAWC = Convert.ToBoolean(growthRegistersData.admittedToAwc);
                        growthRegister.AgeInDays = Convert.ToInt32(growthRegistersData.ageInDays); 
                        growthRegister.AnyDisability = growthRegistersData.anyDisability!=null? growthRegistersData.anyDisability.ToString():null;
                        growthRegister.AnyIllness = growthRegistersData.anyIllness!=null? growthRegistersData.anyIllness.ToString():null;
                        growthRegister.AnyRedFlag = Convert.ToBoolean(growthRegistersData.anyRedFlag);
                        growthRegister.BMI = growthRegistersData.bmi.HasValue ? Convert.ToDouble(growthRegistersData.bmi) : (double?)null;
                        growthRegister.BMIZ = growthRegistersData.bmiz.HasValue ? Convert.ToDouble(growthRegistersData.bmiz) : (double?)null;
                        growthRegister.ChildId = Guid.Parse(growthRegistersData.childId.ToString());
                        growthRegister.CreatedBy = Convert.ToInt32(growthRegistersData.createdBy);
                        growthRegister.CurrentlyAttends = Convert.ToInt32(growthRegistersData.currentlyAttends);
                        growthRegister.DataMonthId = Convert.ToInt32(growthRegistersData.dataMonthId); 
                        growthRegister.Dou = Convert.ToDateTime(growthRegistersData.dou); 
                        growthRegister.GenderID = Convert.ToInt32(growthRegistersData.genderId); 
                        growthRegister.GrowthId = Guid.Parse(growthRegistersData.growthId.ToString());
                        growthRegister.H4AZ = growthRegistersData.h4az.HasValue ? Convert.ToDouble(growthRegistersData.h4az) : (double?)null;
                        growthRegister.HealthCheckUpDone = Convert.ToBoolean(growthRegistersData.healthCheckUpDone);
                        growthRegister.IsBreastfeeding = Convert.ToBoolean(growthRegistersData.isBreastfeeding);
                        growthRegister.IsZScoreRedFlag = Convert.ToBoolean(growthRegistersData.isZscoreRedFlag);
                        growthRegister.LengthHeight = growthRegistersData.lengthHeight.HasValue? Convert.ToDecimal(growthRegistersData.lengthHeight) : (decimal?)null;
                        growthRegister.MeasurementDate = Convert.ToDateTime(growthRegistersData.measurementDate);
                        growthRegister.MUAC = growthRegistersData.muac.HasValue ? Convert.ToDecimal(growthRegistersData.muac) : (decimal?)null; 
                        growthRegister.NumberofDaysIll = Convert.ToInt32(growthRegistersData.numberofDaysIll); 
                        growthRegister.ReasonAnthropometryNotTaken = Convert.ToInt32(growthRegistersData.reasonAnthropometryNotTaken); 
                        growthRegister.ReceiveAAY = Convert.ToInt32(growthRegistersData.receiveAay);
                        growthRegister.ReceiveCookedFood = Convert.ToBoolean(growthRegistersData.receiveCookedFood);
                        growthRegister.ReceiveTakeHomeRation = Convert.ToInt32(growthRegistersData.receiveTakeHomeRation); 
                        growthRegister.Remark = growthRegistersData.remark!=null? growthRegistersData.remark.ToString():null;
                        growthRegister.TypeOfIllness = growthRegistersData.typeOfIllness!=null? growthRegistersData.typeOfIllness.ToString():null;
                        growthRegister.UpdatedBy = Convert.ToInt32(growthRegistersData.updatedBy);
                        growthRegister.W4AZ = growthRegistersData.w4az.HasValue ? Convert.ToDouble(growthRegistersData.w4az) : (double?)null;   
                        growthRegister.W4LHZ = growthRegistersData.w4lhz.HasValue ? Convert.ToDouble(growthRegistersData.w4lhz) : (double?)null;
                        growthRegister.WeightInKg = growthRegistersData.weightInKg.HasValue ? Convert.ToDecimal(growthRegistersData.weightInKg) : (decimal?)null;
                        growthRegister.ChildIllPreviousMonth = Convert.ToBoolean(growthRegistersData.childIllPreviousMonth);
                        growthRegister.ImmunisationCard = growthRegistersData.immunisationCard!=null? growthRegistersData.immunisationCard.ToString():null; 
                        growthRegister.ReceiveAAYBananaInDays = Convert.ToInt32(growthRegistersData.receiveAAYBananaInDays);
                        growthRegister.ReceiveAAYEggInDays = Convert.ToInt32(growthRegistersData.receiveAAYEggInDays);
                        App.DAUtil.SaveGrowthRegister(growthRegister);
                    }
                }
                responseBody = "";
                HttpClient clientRF = new HttpClient();
                HttpResponseMessage responseRF = await clientRF.GetAsync(WebServiceUrl + "api/Sync/SyncGetEntryLevelRedFlagDataForAPP?userId=" + userLoginById.userId);
                HttpContent contentRF = responseRF.Content;
                responseBody = contentRF.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(responseBody))
                {
                    var redFlagRegisters = JsonConvert.DeserializeObject<RootObject>(responseBody);
                    App.DAUtil.DeleteRedFlagRegisterRegister();
                    foreach (var redFlagRegistersData in redFlagRegisters.redFlagRegisters)
                    {
                        RedFlagRegister redFlagRegister = new RedFlagRegister();
                        redFlagRegister.AdvicedButNotAdmittedReason = null; 
                        redFlagRegister.AnyBlockerInLastDiagnose = null; 
                        redFlagRegister.ASHAVisitDate = null;
                        redFlagRegister.AWWVisitWithASHA = null;
                        redFlagRegister.CaseOfReferral = null;
                        redFlagRegister.ChildAdmittedInCTCNRC = Convert.ToInt32(redFlagRegistersData.childAdmittedInCtcnrc);
                        redFlagRegister.ChildGoneToHealthCenter = null; 
                        redFlagRegister.ChildId = Guid.Parse(redFlagRegistersData.childId.ToString()); 
                        redFlagRegister.ChildSeverelyUnderweightSymptoms = redFlagRegistersData.childSeverelyUnderweightSymptoms!=null? redFlagRegistersData.childSeverelyUnderweightSymptoms.ToString():null; 
                        redFlagRegister.ConsultancyProvidedToFamily = null;
                        redFlagRegister.CreatedBy = Convert.ToInt32(redFlagRegistersData.createdBy); 
                        redFlagRegister.CurrentStatus = Convert.ToInt32(redFlagRegistersData.currentStatus);
                        redFlagRegister.DateMonthId = Convert.ToInt32(redFlagRegistersData.dateMonthId);
                       
                        if (redFlagRegistersData.dateofAdmission.HasValue)
                        {
                            StaticClass.dateofAdmission = Convert.ToDateTime(redFlagRegistersData.dateofAdmission);

                            redFlagRegister.DateofAdmission = StaticClass.dateofAdmission.Date;
                          
                        }
                        
                        if (redFlagRegistersData.dateOfDischarge.HasValue)
                        {
                            StaticClass.dateOfDischarge = Convert.ToDateTime(redFlagRegistersData.dateOfDischarge);

                            redFlagRegister.DateOfDischarge = StaticClass.dateOfDischarge.Date;
                            
                        }
                        redFlagRegister.DateOfReferral = null;
                        redFlagRegister.Diagnose = null; 
                        redFlagRegister.Doe = Convert.ToDateTime(redFlagRegistersData.doe);
                        redFlagRegister.Dou = Convert.ToDateTime(redFlagRegistersData.dou);
                        redFlagRegister.FirstFollowUp = Convert.ToBoolean(redFlagRegistersData.firstFollowUp); 
                        redFlagRegister.HygeineMaintained = Convert.ToBoolean(redFlagRegistersData.hygeineMaintained);
                        redFlagRegister.IsEatingSufficientlyBool = redFlagRegistersData.isEatingSufficientlyBool.HasValue ? Convert.ToBoolean(redFlagRegistersData.isEatingSufficientlyBool) : (bool?)null;
                        redFlagRegister.IsSuggestedReferral = redFlagRegistersData.isSuggestedReferral.HasValue? Convert.ToBoolean(redFlagRegistersData.isSuggestedReferral) : (bool?)null;
                        redFlagRegister.IsEatingSufficiently = Convert.ToInt32(redFlagRegistersData.isEatingSufficiently);
                        redFlagRegister.LengthHeight = redFlagRegistersData.lengthHeight.HasValue? Convert.ToDecimal(redFlagRegistersData.lengthHeight) : (decimal?)null;
                        redFlagRegister.MeasurementDate = Convert.ToDateTime(redFlagRegistersData.measurementDate); 
                        redFlagRegister.MUAC = redFlagRegistersData.muac.HasValue? Convert.ToDecimal(redFlagRegistersData.muac) : (decimal?)null; 
                        redFlagRegister.Observation = null; 
                        redFlagRegister.RedFlagId = Guid.Parse(redFlagRegistersData.redFlagId.ToString()); 
                        redFlagRegister.ReferradTo = 0;
                        redFlagRegister.OutcomeofReferralbyASHA = Convert.ToInt32(redFlagRegistersData.outcomeofReferralbyASHA);
                        redFlagRegister.ReferredToHealthCenter = null;
                        redFlagRegister.SecondFollowUp = Convert.ToBoolean(redFlagRegistersData.secondFollowUp); 
                        redFlagRegister.SpecialDietProvided = null;
                        redFlagRegister.ThirdFollowUp = Convert.ToBoolean(redFlagRegistersData.thirdFollowUp); 
                        redFlagRegister.UpdatedBy = Convert.ToInt32(redFlagRegistersData.updatedBy);
                        redFlagRegister.WasOnMedication = Convert.ToInt32(redFlagRegistersData.wasOnMedication);
                        redFlagRegister.WeightInKg = redFlagRegistersData.weightInKg.HasValue ? Convert.ToDecimal(redFlagRegistersData.weightInKg) : (decimal?)null; 
                        redFlagRegister.FourthFollowUp = Convert.ToBoolean(redFlagRegistersData.fourthFollowUp);
                        redFlagRegister.IsCurrentlyIll = Convert.ToBoolean(redFlagRegistersData.isCurrentlyIll);
                        redFlagRegister.Remark = redFlagRegistersData.remark!=null? redFlagRegistersData.remark.ToString():null;
                        App.DAUtil.SaveRedFlagRegister(redFlagRegister);
                     
                    }
                }
                else
                {
                    Flag = true;
                }
            }
            catch (Exception ex)
            {
                App.DAUtil.DeleteUser();
                return Flag = false;
            }
            LastDateInsert();
            return Flag;
        }
         public async Task<bool> PushNewData(PushData pushData)
        {

            bool Flag = false;
            try
            {
                HttpClient client = new HttpClient();
                //HttpClient client = new HttpClient
                //{
                //    Timeout = TimeSpan.FromMinutes(20)
                //};
                var Userdata = App.DAUtil.GetUsers();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Userdata[0].Token);
                // client.Timeout = TimeSpan.FromSeconds(300);
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
                //if(response.IsSuccessStatusCode)
                //{
                //    flageDM = true;
                //}
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
        private void LastDateInsert()
        {
            TblPushDataTime tblPushDataTime = new TblPushDataTime();
            tblPushDataTime.Flage = true;
            tblPushDataTime.CheckDate = DateTime.Now;
            App.DAUtil.SavepushDataTime(tblPushDataTime);
        }
    }
}

public class ChildRegister1
{
    public string childId { get; set; }
    public string familyId { get; set; }
    public string childName { get; set; }
    public DateTime dob { get; set; }
    public int genderId { get; set; }
    public double? birthWeightInKg { get; set; }
    public int? birthOrder { get; set; }
    public DateTime registerDate { get; set; }
    public DateTime doe { get; set; }
    public DateTime dou { get; set; }
    public int createdBy { get; set; }
    public int updatedBy { get; set; }
    public int childStatus { get; set; }
    public double? birthLengthHeightInCms { get; set; }
    public int? birthPlace { get; set; }
    public int? birthDeliveryType { get; set; }
    public int? deliveryTerm { get; set; }
    public double? awcentryWeightInKg { get; set; }
    public double? awcentryHeightInCms { get; set; }
    public double? awcentryMuac { get; set; }
    public double? awcentryW4az { get; set; }
    public double? awcentryW4hz { get; set; }
    public string photograph { get; set; }
    public string childCode { get; set; }
    public int newChildCode { get; set; }
    public DateTime? awcEntryDate { get; set; }
    public string anyDisability { get; set; }
    public string anyIllness { get; set; }
    public string anyLongTermIllnessInFamily { get; set; }
    public int? growthChartGrade { get; set; }
    public int? gradeOfChild { get; set; }
    public object childStatusNavigation { get; set; }
    public object family { get; set; }
    public object gender { get; set; }
    public List<object> growthRegister { get; set; }
    public List<object> redFlagRegister { get; set; }
}
public class FamilyRegister1
{
    public string familyId { get; set; }
    public int? familyType { get; set; }
    public string fatherName { get; set; }
    public int? fatherEducation { get; set; }
    public string fatherOccupation { get; set; }
    public string fatherPastDisease { get; set; }
    public string motherName { get; set; }
    public DateTime? motherDob { get; set; }
    public int? motherEducation { get; set; }
    public string motherOccupation { get; set; }
    public string motherPastDisease { get; set; }
    public int? isMotherWorking { get; set; }
    public int numberofChildenAlive { get; set; }
    public int? numberofChildenDied { get; set; }
    public int? casteTribe { get; set; }
    public string casteTribeName { get; set; }
    public int? religion { get; set; }
    public int? rationCardType { get; set; }
    public bool ownAgriculturalLand { get; set; }
    public int? houseType { get; set; }
    public int? waterSupplyType { get; set; }
    public int? sourceDrinkingWater { get; set; }
    public bool electricityAvailable { get; set; }
    public string assets { get; set; }
    public int locationId { get; set; }
    public DateTime registerDate { get; set; }
    public DateTime doe { get; set; }
    public DateTime dou { get; set; }
    public int createdBy { get; set; }
    public int updatedBy { get; set; }
    public int? motherWorkType { get; set; }
    public int? motherWorkHours { get; set; }
    public int? toiletFacility { get; set; }
    public int? familyMigrateToEarn { get; set; }
    public int? migrationMonthsPerYear { get; set; }
    public int? migrationFor { get; set; }
    public string familyCode { get; set; }
    public int newFamilyCode { get; set; }
    public int status { get; set; }
    public object casteTribeNavigation { get; set; }
    public object familyTypeNavigation { get; set; }
    public object fatherEducationNavigation { get; set; }
    public object houseTypeNavigation { get; set; }
    public object motherEducationNavigation { get; set; }
    public object rationCardTypeNavigation { get; set; }
    public object religionNavigation { get; set; }
    public object waterSupplyTypeNavigation { get; set; }
    public List<object> childRegister { get; set; }
}
public class GrowthRegisterMother1
{
    public string growthId { get; set; }
    public string familyId { get; set; }
    public int dataMonthId { get; set; }
    public bool childExpected { get; set; }
    public DateTime? expectedDeliveryDate { get; set; }
    public bool isFirstPregnancy { get; set; }
    public bool isLactating { get; set; }
    public DateTime? lastDeliveryDate { get; set; }
    public bool awcregistrationDate { get; set; }
    public int ancregistraionDate { get; set; }
    public bool receivedIfatablets { get; set; }
    public bool receivedCalciumTablets { get; set; }
    public bool consumedIfatablets { get; set; }
    public double motherWeightIn1Trimester { get; set; }
    public double motherWeightIn2Trimester { get; set; }
    public double motherWeightInDeliveryTime { get; set; }
    public DateTime dateOfMeasurement { get; set; }
    public bool isGettingTreatment { get; set; }
    public int gettingTreatmentFrom { get; set; }
    public bool isUnderMedication { get; set; }
    public bool receivedDietUnderAay { get; set; }
    public bool receivedMealFromAwcunderAay { get; set; }
    public int? howManyMonthsReceiveMealsAWCUnderAAY { get; set; }
    public int typeOfMeal { get; set; }
    public int eggReceived { get; set; }
    public bool receivedSnacksUnderAay { get; set; }
    public int receivedSnacksFromAwc { get; set; }
    public int eatMealRegularlyFromAwc { get; set; }
    public int isMealEnough { get; set; }
    public int qualityOfAwcfood { get; set; }
    public int resonForNotEatingAwcmeal { get; set; }
    public DateTime doe { get; set; }
    public DateTime dou { get; set; }
    public int createdBy { get; set; }
    public int updatedBy { get; set; }
    public string totalPregnancyMonths { get; set; }
    public string highRiskMotherHistory { get; set; }
    public string ancCheckups { get; set; }
    public string anmMarkHighRiskScreening { get; set; }
    public object dataMonth { get; set; }
}
public class GrowthRegister1
{
    public string growthId { get; set; }
    public string childId { get; set; }
    public int dataMonthId { get; set; }
    public DateTime measurementDate { get; set; }
    public double? weightInKg { get; set; }
    public double? lengthHeight { get; set; }
    public double? muac { get; set; }
    public bool anyRedFlag { get; set; }
    public int receiveTakeHomeRation { get; set; }
    public bool admittedToAwc { get; set; }
    public double? w4lhz { get; set; }
    public double? w4az { get; set; }
    public double? h4az { get; set; }
    public double? bmi { get; set; }
    public double? bmiz { get; set; }
    public bool isZscoreRedFlag { get; set; }
    public int ageInDays { get; set; }
    public int genderId { get; set; }
    public bool isBreastfeeding { get; set; }
    public bool healthCheckUpDone { get; set; }
    public string anyDisability { get; set; }
    public string anyIllness { get; set; }
    public int numberofDaysIll { get; set; }
    public string typeOfIllness { get; set; }
    public int reasonAnthropometryNotTaken { get; set; }
    public int currentlyAttends { get; set; }
    public bool receiveCookedFood { get; set; }
    public int receiveAay { get; set; }
    public string remark { get; set; }
    public DateTime doe { get; set; }
    public DateTime dou { get; set; }
    public int createdBy { get; set; }
    public int updatedBy { get; set; }
    public string immunisationCard { get; set; }
    public bool childIllPreviousMonth { get; set; }
    public object child { get; set; }
    public object dataMonth { get; set; }
    public int receiveAAYEggInDays { get; set; }
    public int receiveAAYBananaInDays { get; set; }
}
public class RedFlagRegister1
{
    public string redFlagId { get; set; }
    public string childId { get; set; }
    public int dateMonthId { get; set; }
    public object referredToHealthCenter { get; set; }
    public object childGoneToHealthCenter { get; set; }
    public object consultancyProvidedToFamily { get; set; }
    public DateTime measurementDate { get; set; }
    public double? weightInKg { get; set; }
    public double? lengthHeight { get; set; }
    public double? muac { get; set; }
    public object specialDietProvided { get; set; }
    public int wasOnMedication { get; set; }
    public object observation { get; set; }
    public object diagnose { get; set; }
    public int currentStatus { get; set; }
    public object anyBlockerInLastDiagnose { get; set; }
    public object ashavisitDate { get; set; }
    public int isEatingSufficiently { get; set; }
    public bool hygeineMaintained { get; set; }
    public object awwvisitWithAsha { get; set; }
    public string childSeverelyUnderweightSymptoms { get; set; }
    public object caseOfReferral { get; set; }
    public object dateOfReferral { get; set; }
    public int referradTo { get; set; }
    public int childAdmittedInCtcnrc { get; set; }
    public DateTime? dateofAdmission { get; set; }
    public DateTime? dateOfDischarge { get; set; }
    public bool firstFollowUp { get; set; }
    public bool secondFollowUp { get; set; }
    public bool thirdFollowUp { get; set; }
    public object advicedButNotAdmittedReason { get; set; }
    public int createdBy { get; set; }
    public int updatedBy { get; set; }
    public DateTime doe { get; set; }
    public DateTime dou { get; set; }
    public object child { get; set; }
    public object dateMonth { get; set; }
    public bool fourthFollowUp { get; set; }
    public bool isCurrentlyIll { get; set; }
    public string remark { get; set; }
    public int outcomeofReferralbyASHA { get; set; }
    public bool? isEatingSufficientlyBool { get; set; }
    public bool? isSuggestedReferral { get; set; }
}
public class RootObject
{
    public List<ChildRegister1> childRegisters { get; set; }
    public List<FamilyRegister1> familyRegisters { get; set; }
    public List<GrowthRegister1> growthRegisters { get; set; }
    public List<RedFlagRegister1> redFlagRegisters { get; set; }
    public List<GrowthRegisterMother1> growthRegisterMothers { get; set; }
}
