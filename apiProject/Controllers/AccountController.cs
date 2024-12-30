using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiProject.Interfaces;
using apiProject.DTOs;
using apiProject.Models;
using apiProject.Repository;
using apiProject.Mappers;
using apiProject.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using apiProject.Validators;
using FluentValidation;
using apiProject.Helpers;




namespace apiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class AccountController : ControllerBase
    {

        private readonly IAccountRepository _AccountRepo;

        

        public AccountController(IAccountRepository account)
        {
            _AccountRepo = account;

        }


        [HttpPost]
        public async Task<IActionResult> AddAcount([FromBody] AccountDto accountDto){
            var validator = new AccountDtoValidator();
            var validationResult = validator.Validate(accountDto);

            if(!validationResult.IsValid){
                return BadRequest(validationResult.Errors);
            }
            var ModelAccount = await _AccountRepo.Add(accountDto);

            var accountDtoResponse = new
            {
                ModelAccount.Service,
                ModelAccount.Username,
                ModelAccount.Password,
                CreatedAt = ModelAccount.DateAdded
            };

            return CreatedAtAction(nameof(GetAccountById), new { id = ModelAccount.Id }, accountDtoResponse);
        }


        
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                // Récupérer tous les comptes
                var allAccounts = await _AccountRepo.GetAll();

                // Limiter à 10 comptes
                var accountsToDisplay = allAccounts.Take(10).ToList();

                // Appliquer la transformation DTO
                var accountDtos = accountsToDisplay.Select(x => x.ToAccountDto()).ToList();

                // Retourner la liste avec un code 200 OK
                return Ok(accountDtos);
            }
            catch (AccountNotFoundException ex)
            {
                return HandlError(ex);
            }
        }



        [HttpGet("{id}")] // Retourner Account par ID
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            try
            {
                // Appel de la méthode GetById de ton repository, elle lancera une exception si l'account est introuvable
                var accountById = await _AccountRepo.GetById(id);
                
                return Ok(accountById);
            }
            catch(AccountNotFoundException ex){
                return HandlError(ex);
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> GetAccountByServiceOrName([FromQuery] QueryObject query)
        {
            try
            {
                var allAccounts = await _AccountRepo.GetAccountByServiceAndName(query);

                // Appliquer la transformation DTO
                var accountDtos = allAccounts.Select(x => x.ToAccountDto()).ToList();

                

                // Retourner la liste avec un code 200 OK
                return Ok(accountDtos);
            }
            catch(AccountNotFoundException ex){
                return HandlError(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id,[FromBody] AccountUpdateDto accountupdateDto)
        {
            var validator = new AccountUpdateDtoValidator();
            var validationResult = validator.Validate(accountupdateDto); 
            
            if(!validationResult.IsValid){
                return BadRequest(validationResult.Errors);
            }
        
            try{
                await _AccountRepo.Update(id, accountupdateDto);
                return Ok(accountupdateDto);
            }
            catch(AccountNotFoundException ex){
                return HandlError(ex);
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            try{
                var DeleteAccount = await _AccountRepo.Delete(id);
                // Retourner un statut 200 (OK) avec un message de confirmation
                return Ok(new { message = "Your Account Has Been Deleted" });
            }
            catch(AccountNotFoundException ex){
                return HandlError(ex);
            }
        }

        
        


        private IActionResult HandlError(Exception ex){
            if(ex is AccountNotFoundException){
                return NotFound(ex.Message);
            }
            return StatusCode(500, new { Message = "⚠️⚠️⚠️⚠️ Serveur En Cours De Maintenance ⚠️⚠️⚠️⚠️", Details = ex.Message });
        }
    }

}
