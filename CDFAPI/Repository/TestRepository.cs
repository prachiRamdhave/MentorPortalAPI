using CDFAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Repository
{
    public class TestRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<Assesment> GetAssessment(int langID)
        {
            List<Assesment> lst = new List<Assesment>();
            try
            {
                Assesment d = new Assesment();
                //  HttpContext.GetLocalResourceObject("~/Test/App_LocalResources/assessment.aspx.resx", "key1").ToString();
                if (langID == 1)
                {
                    d.btnPersonality = "Start Test";
                    d.lblAssesment = "Test Details";
                    d.lblHeading = "Assessment Detail";
                    d.lblInfo = "Personality test consists of 2 tests. Answering all questions is mandatory and this test is not time bound. 1st test comprises of 90 questions which helps in understanding your personality and characteristics. 2nd test consists of 24 questions which helps in understanding the behavior of an individual.";
                    d.lblMainMsg = "This is a 2 step test process, Know Your Self and Personality Test.";

                    lst.Add(d);
                    return lst;
                }
                if (langID == 2)
                {
                    d.btnPersonality = "परीक्षण प्रारंभ";
                    d.lblAssesment = "परीक्षण विवरण";
                    d.lblHeading = "आकलन विस्तार";
                    d.lblInfo = "इस मूल्यांकन में 2 परीक्षायें हैं। सभी सवालों के जवाब देना अनिवार्य है और यह मूल्यांकन समय बाध्य नहीं है। पहेले परीक्षण में ९० सवाल हैं, जो आपके व्यक्तित्व और विशेषताओं को समझने में मदद करता है, और दूसरे परीक्षण में २४ सवाल हैं, जो व्यक्ति के व्यवहार को समझने में मदद करता है।";
                    d.lblMainMsg = "यह २ परीक्षण  का संग्रह है , खुद को पहचाने और व्यक्तिव परीक्षण . दिये गये अनुक्रम में परीक्षण पूरा करें. यह परीक्षा बहुभाषी है, अपनी पसंदीदा भाषा का चयन करें कृपया क्रम में परीक्षा को पूरा करें";

                    lst.Add(d);
                    return lst;
                }
                if (langID == 3)
                {
                    d.btnPersonality = "चाचणी प्रारंभ";
                    d.lblAssesment = "चाचणी तपशील";
                    d.lblHeading = "मूल्यमापन तपशील";
                    d.lblInfo = "व्यक्तिमत्व चाचणीमध्ये २ चाचण्यांचा समावेश आहे. सर्व प्रश्नांची उत्तरे देणे बंधनकारक आहे व या चाचणीला वेळेचे बंधन नाही. पहिल्या चाचणीमध्ये ९० प्रश्नांचा समावेश आहे, ज्यामुळे तुमचे व्यक्तिमत्व व वैशिष्ट्ये समजून घ्यायला मदत होते. दुसऱ्या चाचणीमध्ये २४ प्रश्नांचा समावेश आहे, ज्यामुळे एखाद्या व्यक्तिचे वर्तन समजून घ्यायला मदत होते.";
                    d.lblMainMsg = "ही  2 चरण चाचणी प्रक्रिया आहे, स्वतःला ओळखा आणि व्यक्तिमत्व चाचणी . चाचणी दिलेल्या क्रमामध्ये पूर्ण करा. ही चाचणी बहुभाषी आहे, आपली पसंत केलेली भाषा निवडा चाचणी क्रमाने पूर्ण करा";
                    lst.Add(d);
                    return lst;
                }
                if (langID == 4)
                {
                    d.btnPersonality = "ટેસ્ટ પ્રારંભ કરો";
                    d.lblAssesment = "ટેસ્ટ વિગતો";
                    d.lblHeading = "આકારણી વિગત";
                    d.lblInfo = "પર્સનાલિટી ટેસ્ટમાં 2 પરીક્ષણો છે. બધા પ્રશ્નોના જવાબ ફરજિયાત છે અને આ કસોટી સમય બાઉન્ડ નથી. 1 લી ટેસ્ટમાં 90 પ્રશ્નોનો સમાવેશ થાય છે જે તમારા વ્યક્તિત્વ અને લક્ષણોને સમજવામાં મદદ કરે છે. બીજી ટેસ્ટમાં 24 પ્રશ્નો છે, જે વ્યક્તિગત વર્તણૂકને સમજવામાં મદદ કરે છે.";
                    d.lblMainMsg = "આ એક 2 પગલું પરીક્ષણ પ્રક્રિયા છે, તમારા સ્વ અને પર્સનાલિટી ટેસ્ટ જાણો. આ પરીક્ષણ બહુભાષી છે, તમારી પસંદ કરેલી ભાષા પસંદ કરો કૃપા કરીને શ્રેણીમાં પરીક્ષણ પૂર્ણ કરો";
                    lst.Add(d);
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public List<SeparateTestStatus> GetSeparate_Pesonality_TestStatus(int langID)
        {
            List<SeparateTestStatus> lst = new List<SeparateTestStatus>();
            try
            {
                SeparateTestStatus st = new SeparateTestStatus();
                if (langID == 1)
                {
                    st.click = "Click Here to Start";
                    st.Heading = "Test Dashboard";
                    st.know = "Know Your Self";
                    st.no1 = "1";
                    st.no2 = "2";
                    st.Note = "This test is collection of two tests. These are also not time bound tests. So be true about yourself and enjoy tests.";
                    st.NoteM = "Note :";
                    st.notime = "No. of time";
                    st.persn = "Test Details";
                    st.personality = "Personality Test";
                    st.status = "Status";

                    lst.Add(st);
                    return lst;
                }
                if (langID == 2)
                {
                    st.click = "शुरू करने के लिए यहां क्लिक करें";
                    st.Heading = "परीक्षण डैशबोर्ड";
                    st.know = "खुद को जानिए";
                    st.no1 = "१.";
                    st.no2 = "२.";
                    st.Note = "इस परीक्षण में 2 परीक्षायें हैं। यह परीक्षण भी समयबद्ध नहीं है। तो खुद के बारे में ईमानदार रहे और परीक्षण का आनंद लें।";
                    st.NoteM = "फ़ुटनोट  :";
                    st.notime = "गिनती";
                    st.persn = "परीक्षण विवरण";
                    st.personality = "व्यक्तित्व परीक्षण";
                    st.status = "स्थिति";

                    lst.Add(st);
                    return lst;
                }
                if (langID == 3)
                {
                    st.click = "प्रारंभ करण्यासाठी येथे क्लिक करा";
                    st.Heading = "चाचणी उपकरणफलक";
                    st.know = "स्वतःला ओळखा";
                    st.no1 = "१.";
                    st.no2 = "२.";
                    st.Note = "या चाचणीमध्ये २ चाचण्यांचा समावेश आहे. या चाचण्या देखील कालबद्ध नाहीत. त्यामुळे स्वत: बद्दल प्रामाणिक राहा आणि चाचण्याचा आनंद घ्या.";
                    st.NoteM = "सूचना :";
                    st.notime = "अवधि";
                    st.persn = "चाचणी विवरण";
                    st.personality = "व्यक्तिमत्व चाचणी";
                    st.status = "स्थिति";

                    lst.Add(st);
                    return lst;
                }
                if (langID == 4)
                {
                    st.click = "પ્રારંભ કરવા માટે અહીં ક્લિક કરો";
                    st.Heading = "વ્યક્તિત્વ કસોટીવ્યક્તિત્વ કસોટી ડેશબોર્ડ";
                    st.know = "ખુદને જાણો";
                    st.no1 = "૧.";
                    st.no2 = "૨.";
                    st.Note = "પર્સનાલિટી ટેસ્ટ બે પરીક્ષણોનો સંગ્રહ છે. આ સમય બાઉન્ડ ટેસ્ટ પણ નથી. તેથી તમારા વિશે સાચું છે અને પરીક્ષણો આનંદ";
                    st.NoteM = "નૉૅધ :";
                    st.notime = "સમયની સંખ્યા";
                    st.persn = "ટેસ્ટ વિગતો";
                    st.personality = "વ્યક્તિત્વ કસોટ";
                    st.status = "સ્થિતિ";

                    lst.Add(st);
                    return lst;
                }
                else
                {

                    return null;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }
    }
}