﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jobsy_API.Controllers;
using ENTITY_L.Models.Employer;


namespace Jobsy.Controllers
{
    public class EmployerController : Controller
    {

        EmployerAPIController Employer = new EmployerAPIController();

        // GET: Employer
        public ActionResult EmployerDashboard()
        {
            try
            {
                // Verification.
                if (Request.IsAuthenticated && ClaimsPrincipal.Current.FindFirst(ClaimTypes.Role).Value == "Employer")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<EmployerModel>> LoadEmployerAsync() // Metodo para devolver una vista con todos los empleaos 
        {
            try
            {
                return await Employer.LoadEmployerAsync(); //Guardamos el resultado del metodo en el Viewbag
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoadEmployer(string id)
        {
            try
            {
                ViewBag.aEmployer = await EmployerLoad(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<EmployerModel> EmployerLoad(string id)
        {
            try
            {
                EmployerModel aEmployer = await Employer.LoadEmployer(id);
                return aEmployer;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EditUser(EmployerModel Employertoedit)
        {
            try
            {
                await Edit(Employertoedit);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("LoadUsersAsync");
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<EmployerModel> Edit(EmployerModel Employertoedit)
        {
            await Employer.EditEmployer(Employertoedit);
            return Employertoedit;
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult DeleteUser(string id)
        {
            try
            {
                Delete(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("LoadUsersAsync");
        }

        public void Delete(string id)
        {
            try
            {

                Employer.DeleteEmployer(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
    }
}