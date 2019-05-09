using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Sort_Web_App.Models;

namespace Data_Sort_Web_App.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        //POST: sort data through selected data type by bubble sorting algorithum
        [HttpPost]
        public ActionResult Index([Bind(Include = "input, valueType")] Sort sort)
        {
            //validating the model before preceding further
            if (ModelState.IsValid)
            {
                //string array is prepared through spliting it by commas
                string[] splitted = sort.input.Split(',');
                //length of the array
                int length = splitted.Length;
                //returning to home if length of array is less than 1 or more than 500
                if(length < 1 || length > 500)
                {
                    return View("Index");
                }

                //checking selected data type
                //list to capture the sorting steps 
                List<string> steps = new List<string>();

                if (sort.valueType.ToString().Equals("Int"))
                {
                    //declared integer array with required length
                    int[] numbers = new int[length];

                    //limiting to only integers (preventing unnecessary crashes)
                    try
                    {
                        numbers = sort.input.Split(',').Select(x => int.Parse(x)).ToArray();
                    }
                    catch
                    {
                        return View("Index");
                    }
                    //temparory integer variable to hold the temp value during the movement of values
                    int temp;
                    //Bubble Sorting algorithum
                    for (int j = 0; j <= length - 2; j++)
                    {
                        for (int i = 0; i <= length - 2; i++)
                        {
                            if (numbers[i] > numbers[i + 1])
                            {
                                temp = numbers[i + 1];
                                numbers[i + 1] = numbers[i];
                                numbers[i] = temp;
                                //Adding current array to list
                                steps.Add(string.Join(",", numbers));
                            }

                        }
                    }
                    //defined object in order to pass model to view
                    SortingSteps SP = new SortingSteps();
                    //given the list reference
                    SP.ArraySteps = steps;
                    //return to TempView along with model
                    return View("IntView", SP);
                }
                else if (sort.valueType.ToString().Equals("String"))
                {
                    //declared string array with required length
                    string[] stringArray = new string[length];
                    
                    //limiting to only string (preventing unnecessary crashes)
                    try
                    {
                        stringArray = sort.input.Split(',').Select(x => x.ToString()).ToArray();
                    }
                    catch
                    {
                        return View("Index");
                    }
                    //Additional check if it contains integer than it will redirect to home
                    foreach (char value in sort.input)
                    {
                        if (char.IsDigit(value))
                        {
                            return View("Index");
                        }
                    }


                    //return to home page if length of any word is more than 10
                    for (int x=0; x<length; x++)
                    {
                        if(stringArray[x].ToString().Length > 10)
                        {
                            return View("Index");
                        }
                    }

                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length - i - 1; j++)
                        {
                            if (stringArray[j] != null && stringArray[j + 1] != null && stringArray[j].CompareTo(stringArray[j + 1]) > 0)
                            {
                                string temp = stringArray[j];
                                stringArray[j] = stringArray[j + 1];
                                stringArray[j + 1] = temp;
                                steps.Add(string.Join(" | ", stringArray));
                                //steps.Add(string.Join(",", stringArray.Select(x => string.Format("'{0}'", x)).ToList()));
                            }
                        }
                    }
                    //defined object in order to pass model to view
                    SortingSteps SP = new SortingSteps();
                    //given the list reference
                    SP.ArraySteps = steps;
                    //return to TempView along with model
                    return View("StringView", SP);
                }
            }

            return RedirectToAction("Index");
        }
    }
}

