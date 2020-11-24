using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace jQueryWrapper
{
    public abstract class jQuery
    {
        private int waitTime = 30000;

        private string libraryContent;

        internal int hObject = -1;


        internal jQuery()
        {

        }

        public jQuery(string version = "3.1.1")
        {
            using (WebClient wc = new WebClient())
                libraryContent = wc.DownloadString("http://code.jquery.com/jquery-" + version + ".js");
        }


        public abstract jQuery Clone(int hObject);

        public abstract void LoadLibrary();

        public abstract object InvokeScript(string functionName, object[] parameters);



        internal string ObjectArrayToString(object[] foo)
        {
            string result;


            result = "";

            foreach (object o in foo)
            {
                if (result.Length > 0)
                    result += ",";

                if (o is string)
                {
                    result += "\"" + (o as string).Replace("\"", "\\\"") + "\"";
                }
                else if (o is int || o is double || o is float || o is bool)
                {
                    result += o.ToString();
                }
                else
                {
                    throw new Exception("Unknown data type passed as parameter");
                }
            }

            result = "[" + result + "]";

            return result;
        }

        private object CallMember(string functionName, string name, string value)
        {
            return CallMember(functionName, new object[] { name, value });
        }

        private object CallMember(string functionName, string value)
        {
            return CallMember(functionName, new object[] { value });
        }

        private object CallMember(string functionName)
        {
            return CallMember(functionName, new object[] { });
        }

        public object CallMember(string functionName, object[] funcParams)
        {
            return this.InvokeScript("genjQueryExecuteFunction", new object[] { this.hObject, functionName, ObjectArrayToString(funcParams) });
        }

        public object CallMemberRaw(string functionName, object[] funcParams)
        {
            object result;


            result = null;

            this.LoadLibrary();
            this.InvokeScript("genjQueryExecuteFunctionRaw", new object[] { this.hObject, functionName, ObjectArrayToString(funcParams) });
     
            return result;
        }



        // Setters

        public void SetWaitTime(int time)
        {
            this.waitTime = time;
        }

        public bool WaitForElement(string query)
        {
            DateTime startTime;
            
            bool ret;

            startTime = DateTime.Now;

            ret = false;

            while ((DateTime.Now - startTime).TotalMilliseconds < this.waitTime)
            {
                if (this.find(query).length != 0)
                {
                    ret = true;
                    break;
                }
                else
                    Thread.Sleep(1000);
            }

            return ret;
        }


        public jQuery Init(string query)
        {
            jQuery el;

            object result;


            result = null;
            el = null;

            this.LoadLibrary();
            try
            {
                //"return genjQueryInit.apply(null, arguments);"
                result = this.InvokeScript("genjQueryInit", new string[] { query });
            }
            catch { }

            if (result != null)
            {
                int handle = Convert.ToInt32(result);

                el = this.Clone(handle);
            }
            else
            {
                el = this.Clone(-1);

                // fail silently
                //throw new Exception("Failed to execute query " + query);
            }
      
            return el;
        }

        public jQuery find(string query)
        {
            jQuery el;


            if (this.hObject == -1)
            {
                el = Init(query);
            }
            else
            {
                el = this.Clone(Convert.ToInt32(CallMember("find", query)));
            }

            return el;
        }

        public jQuery css(string name, string value)
        {
            CallMember("css", name, value);

            return this;
        }

        public jQuery val(string value)
        {
            CallMember("val", value);

            return this;
        }

        public jQuery html(string value)
        {
            CallMember("html", value);

            return this;
        }

        public jQuery attr(string name, string value)
        {
            CallMember("attr", name, value);

            return this;
        }

        public jQuery prepend(string query)
        {
            return Clone(Convert.ToInt32(CallMember("prepend", query)));
        }

        public jQuery prepend(jQuery el)
        {
            return Clone(Convert.ToInt32(CallMember("prepend", new object[] { el.hObject })));
        }

        public jQuery append(string query)
        {
            return Clone(Convert.ToInt32(CallMember("append", query)));
        }

        public jQuery append(jQuery el)
        {
            return Clone(Convert.ToInt32(CallMember("append", new object[] { el.hObject })));
        }

        public jQuery removeAttr(string attr)
        {
            return Clone(Convert.ToInt32(CallMember("removeAttr", attr)));
        }

        public jQuery scrollTop(int top)
        {
            return Clone(Convert.ToInt32(CallMember("scrollTop", new object[] { top.ToString() })));
        }

        public jQuery addClass(string className)
        {
            return Clone(Convert.ToInt32(CallMember("addClass", className)));
        }

        public jQuery removeClass(string className)
        {
            return Clone(Convert.ToInt32(CallMember("removeClass", className)));
        }

        public jQuery toggleClass(string className)
        {
            return Clone(Convert.ToInt32(CallMember("toggleClass", className)));
        }

        // Doesn't do default action of invoking jquery event called click
        // instead of calls the actual click function of the html elements
        public jQuery click()
        {
            CallMemberRaw("click", new object[] { });

            return this;
        }


        // Getter

        public int width()
        {
            return Convert.ToInt32(CallMember("width"));
        }

        public int height()
        {
            return Convert.ToInt32(CallMember("height"));
        }

        public int index()
        {
            return Convert.ToInt32(CallMember("index"));
        }

        public jQuery first()
        {
            return Clone(Convert.ToInt32(CallMember("first")));
        }

        public jQuery last()
        {
            return Clone(Convert.ToInt32(CallMember("last")));
        }

        public jQuery children()
        {
            return Clone(Convert.ToInt32(CallMember("children")));
        }

        public jQuery children(string filter)
        {
            return Clone(Convert.ToInt32(CallMember("children", filter)));
        }

        public jQuery parent()
        {
            return Clone(Convert.ToInt32(CallMember("parent")));
        }

        public jQuery parents()
        {
            return Clone(Convert.ToInt32(CallMember("parents")));
        }

        public jQuery parents(string filter)
        {
            return Clone(Convert.ToInt32(CallMember("parents", filter)));
        }

        public jQuery filter(string filter)
        {
            return Clone(Convert.ToInt32(CallMember("filter", filter)));
        }

        public jQuery next()
        {
            return Clone(Convert.ToInt32(CallMember("next")));
        }

        public jQuery prev()
        {
            return Clone(Convert.ToInt32(CallMember("prev")));
        }

        public jQuery remove()
        {
            return Clone(Convert.ToInt32(CallMember("remove")));
        }

        public jQuery eq(int index)
        {
            return Clone(Convert.ToInt32(CallMember("eq", index.ToString())));
        }

        public bool hasClass(string className)
        {
            return (bool)CallMember("hasClass", className);
        }

        public bool is_(string query)
        {
            return (bool)CallMember("is", query);
        }

        public bool prop(string name, object value)
        {
            if (value is int)
                value = value.ToString();

            return (bool)CallMember("prop", new object[] { name, value });
        }

        public int scrollTop()
        {
            return Convert.ToInt32(CallMember("scrollTop"));
        }


        public delegate bool EachCallback(int index, jQuery el);

        /// <summary>
        /// Iterates through each element in the object
        /// </summary>
        /// <param name="cb">must return true to continue iteration</param>
        /// <returns></returns>
        public jQuery each(EachCallback cb)
        {
            for (int i = 0; i < this.length; i++)
            {
                if (cb(i, this.eq(i)) == false)
                    break;
            }

            return this;
        }

        public string val()
        {
            return CallMember("val") as string;
        }

        public string html()
        {
            return CallMember("html") as string;
        }

        public string text()
        {
            return CallMember("text") as string;
        }

        public string attr(string name)
        {
            return CallMember("attr", name) as string;
        }



        public int length
        {
            get
            {
                int elLength = 0;

                object result;

                result = this.InvokeScript("genjQueryGetMember", new object[] { this.hObject, "length" });

                if (result != null)
                {
                    elLength = Convert.ToInt32(result);
                }
                else
                {
                    // fail silently
                    //throw new Exception("Failed to get legnth");
                }

                return elLength;
            }
        }
    }
}


