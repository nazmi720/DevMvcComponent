﻿#region using block

using System;
using System.Reflection;

#endregion

namespace DevMvcComponent {
    /// <summary>
    ///     Must setup this class.
    /// </summary>
    public static class Config {
        ///// <summary>
        /////     System admin email
        ///// </summary>
        //public static string AdminEmail = null;

        private static string _applicationMailFooter = "";
        private static string _commonStyles = ";margin-top: 12px; padding: 11px; border-radius: 4px;'";
        /// <summary>
        ///     Developer email
        /// </summary>
        public static string[] DeveloperEmails = null;

        /// <summary>
        ///     Sets Assembly = Assembly.GetExecutingAssembly();
        /// </summary>
        public static Assembly Assembly = null;

        /// <summary>
        ///     Running application name
        /// </summary>
        public static string ApplicationName = null;

        /// <summary>
        ///     Notify Developer on Error if true.
        /// </summary>
        public static bool IsNotifyDeveloper = true;

        public static string CommonStyles {
            get { return _commonStyles; }
            set { _commonStyles = value; }
        }

        public static string GetAssemblyAttribute<T>(Func<T, string> value)
                                    where T : Attribute {
            T attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
        public static string GetAssemblyAttribute<T>(Assembly assembly,Func<T, string> value)
                                  where T : Attribute {
                                      T attribute = (T)Attribute.GetCustomAttribute(assembly, typeof(T));
            return value.Invoke(attribute);
        }

        public static string GetBold(string str) {
            return "<strong title='" + str + "'>" + str + "</strong>";
        }
        /// <summary>
        ///     Attach application information from the AssemblyInfo given the Assembly = Assembly.GetExecutingAssembly().
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationNameHtml(bool isForce = false) {

            //styleStart = "style='",
            //colorRed = "color:red",
            //styleJoiner = ";",
            //quoteClose = "'";
            var component = Assembly.GetExecutingAssembly();
            if (Assembly != null && (_applicationMailFooter == "" || isForce)) {
                string str = "",
                       divStart = "<div",
                       slashClose = ">",
                       divClose = "</div>";
                str += divStart + " style='background:#5D5A5A;color:white" + _commonStyles + slashClose;

                str += divStart + slashClose;
                str += "Application Title : ";
                str += GetBold(GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly, a => a.Title));
                str += divClose;

                str += divStart + slashClose;
                str += "Application Name : ";
                str += GetBold(Assembly.FullName);
                str += divClose;

                str += divStart + slashClose;
                str += "Application Location : ";
                str += Assembly.Location;
                str += divClose;

                str += divStart + slashClose;
                str += "Application Version : ";
                str += Assembly.GetName().Version;
                str += divClose;

                str += divClose;

                str += divStart + " style='background: #9CB0C5; color: white" + _commonStyles + slashClose;

                str += divStart + slashClose;
                str += "Component Name : ";
                str += GetBold(GetAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title));
                str += divClose;

                str += divStart + slashClose;
                str += "Component Developed Company Name: ";
                str += GetAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company);
                str += divClose;

                str += divStart + slashClose;
                str += "Component Trademark: ";
                str += GetBold(GetAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark));
                str += divClose;

                str += divStart + slashClose;
                str += "Component Developer's Email: ";
                str += "<a href='mailto:me@alimkarim.com'>me@alimkarim.com</a>";
                str += divClose;


                str += divStart + slashClose;
                str += "Documentation: ";
                str += "<a href='https://github.com/aukgit/DevMVCComponent'>https://github.com/aukgit/DevMVCComponent</a>";
                str += divClose;

                str += divStart + slashClose;
                str += "Component Location : ";
                str += component.Location;
                str += divClose;

                str += divStart + slashClose;
                str += "Component Version : ";
                str += component.GetName().Version;
                str += divClose;

                str += divClose;

                _applicationMailFooter = str;
                return _applicationMailFooter;
            }

            return _applicationMailFooter;
        }
    }
}