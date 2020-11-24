using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
                        
namespace jQueryWrapper.Selenium
{
    public class jQuerySE : jQueryWrapper.jQuery
    {
        IJavaScriptExecutor scriptExecutor;

        private string libraryContent;


        internal jQuerySE()
        {

        }

        public jQuerySE(IJavaScriptExecutor scriptExecutor, string version = "3.1.1") : base(version)
        {
            using (WebClient wc = new WebClient())
                libraryContent = wc.DownloadString("http://code.jquery.com/jquery-" + version + ".js");

            // Save it
            this.scriptExecutor = scriptExecutor;
        }


        public override jQuery Clone(int hObject)
        {
            jQuerySE result;


            result = new jQuerySE();
            result.hObject = hObject;
            result.scriptExecutor = this.scriptExecutor;

            return result;
        }

        public override object InvokeScript(string functionName, object[] parameters)
        {
            object result = null;

            try
            {
                result = scriptExecutor.ExecuteScript("return " + functionName + ".apply(null, arguments);", parameters);
            }
            catch(Exception x)
            {
                x.ToString();
            }

            return result;
        }

        public override void LoadLibrary()
        {
            // Inject our jQuery when it hasn't been loaded already

            if ((this.scriptExecutor.ExecuteScript("return document.getElementById(\"genjQuery\") == null", null) as bool?) == true)
            {
                // Must be no conflicted before anything else is allowed to execute
                // to prevent race conditions since javascript files are loaded async
                this.scriptExecutor.ExecuteScript(
                @"
                // Wrap it in an anonymous function
                (function()
                {
                    // include the jQuery library
                    " + libraryContent + @"
                        
                    // Release the jQuery object
                    window.genjQuery = $.noConflict(true);

                    // todo - To prevent trying to access the wrong jQuery object on a page that has changed
                    window.genDocumentId = new Date().getDate();


                    window.genjQuery(""head"").first().append(""<script id=\""genjQuery\""></script>"");
                    window.genjQueryIndex = 0;
                    window.genjQueryObjects = Array();

                    window.genjQueryInit = function(query)
                    {
                        if(query == ""document"")
                        {
                            query = document;
                        }

                        else if (query == ""window"")
                        {
                            // alert(""Converted to window object"");
                            query = window;
                        }

                        var el = window.genjQuery(query);

                        // return handle to jQuery object that was created
                        return addGenjQueryObjectToArray(el);
                    }

                    window.addGenjQueryObjectToArray = function(obj)
                    {
                        var index = window.genjQueryIndex;

                        window.genjQueryObjects[index] = obj;

                        window.genjQueryIndex++;

                        return index;
                    }

                    window.genjQueryExecuteFunctionRaw = function(hObject, functionName, params)
                    {
                        if (typeof(params) != ""undefined"")
                            params = eval(params);
                        else
                            params = Array();

                        var el = window.genjQueryObjects[hObject];

                        var result;

                        el.each(function(index, el2)
                        {
                            var func = el2[functionName];

                            result = func.apply(el2, params);
                        });

                        return result;
                    }

                    window.genjQueryExecuteFunction = function(hObject, functionName, params)
                    {
                        if (typeof(params) != ""undefined"")
                        {
                            params = eval(params);
		
                            for(var key in params)
                            {
                                var val = params[key];
			
                                if(typeof(val) == ""number"")
                                {
                                    val = window.genjQueryObjects[val];
                                }
			
                                params[key] = val;
                            }
                        }
                        else
                        {
                            params = Array();
                        }
	
                        var el = window.genjQueryObjects[hObject];

                        var func = el[functionName];

                        var result = func.apply(el, params);
	
                        if(result instanceof window.genjQuery)
                            result = addGenjQueryObjectToArray(result);
	
                        return result;
                    }

                    window.genjQueryGetMember = function(hObject, memberName)
                    {
                        var el = window.genjQueryObjects[hObject];

                        return el[memberName];
                    }

                })();
                ", null);
            }
        }


        public jQuerySE Init(string query)
        {
            return base.Init(query) as jQuerySE;
        }

        public jQuerySE find(string query)
        {
            return base.find(query) as jQuerySE;
        }

        public jQuerySE css(string name, string value)
        {
            return base.css(name, value) as jQuerySE;
        }

        public jQuerySE val(string value)
        {
            return base.val(value) as jQuerySE;
        }

        public jQuerySE html(string value)
        {
            return base.html(value) as jQuerySE;
        }

        public jQuerySE attr(string name, string value)
        {
            return base.attr(name, value) as jQuerySE;
        }

        public jQuerySE prepend(string query)
        {
            return base.prepend(query) as jQuerySE;
        }

        public jQuerySE prepend(jQuerySE el)
        {
            return base.prepend(el) as jQuerySE;
        }

        public jQuerySE append(string query)
        {
            return base.append(query) as jQuerySE;
        }

        public jQuerySE append(jQuerySE el)
        {
            return base.append(el) as jQuerySE;
        }

        public jQuerySE removeAttr(string attr)
        {
            return base.removeAttr(attr) as jQuerySE;
        }

        public jQuerySE scrollTop(int top)
        {
            return base.scrollTop(top) as jQuerySE;
        }

        public jQuerySE addClass(string className)
        {
            return base.addClass(className) as jQuerySE;
        }

        public jQuerySE removeClass(string className)
        {
            return base.removeClass(className) as jQuerySE;
        }

        public jQuerySE toggleClass(string className)
        {
            return base.toggleClass(className) as jQuerySE;
        }

        public jQuerySE click()
        {
            return base.click() as jQuerySE;
        }


        public jQuerySE first()
        {
            return base.first() as jQuerySE;
        }

        public jQuerySE last()
        {
            return base.last() as jQuerySE;
        }

        public jQuerySE children()
        {
            return base.children() as jQuerySE;
        }

        public jQuerySE children(string filter)
        {
            return base.children(filter) as jQuerySE;
        }

        public jQuerySE parent()
        {
            return base.parent() as jQuerySE;
        }

        public jQuerySE parents()
        {
            return base.parents() as jQuerySE;
        }

        public jQuerySE parents(string filter)
        {
            return base.parents(filter) as jQuerySE;
        }

        public jQuerySE filter(string filter)
        {
            return base.filter(filter) as jQuerySE;
        }

        public jQuerySE next()
        {
            return base.next() as jQuerySE;
        }

        public jQuerySE prev()
        {
            return base.prev() as jQuerySE;
        }

        public jQuerySE remove()
        {
            return base.remove() as jQuerySE;
        }

        public jQuerySE eq(int index)
        {
            return base.eq(index) as jQuerySE;
        }

        public jQuerySE each(EachCallback cb)
        {
            return base.each(cb) as jQuerySE;
        }
    }
}
