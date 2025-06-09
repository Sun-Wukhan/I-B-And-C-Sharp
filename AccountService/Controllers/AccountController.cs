using Microsoft.AspNetCore.Mvc;
using AccountService.Services;
using Fundamentals;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _repo;

    public AccountController(IAccountRepository repo) =>
        _repo = repo;

    [HttpGet]
    public IEnumerable<BankAccount> GetAll() =>
        _repo.GetAll();

    [HttpGet("{id}")]
    public ActionResult<BankAccount> Get(string id) =>
        _repo.Get(id) is var acct && acct is not null
            ? Ok(acct)
            : NotFound();

    [HttpPost]
    public ActionResult Create(BankAccount acct)
    {
        _repo.Create(acct);
        return CreatedAtAction(nameof(Get), new { id = acct.AccountNumber }, acct);
    }

    [HttpPut("{id}")]
    public ActionResult Update(string id, BankAccount acct)
    {
        if (id != acct.AccountNumber) return BadRequest();
        return _repo.Update(acct) 
            ? NoContent() 
            : NotFound();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id) =>
        _repo.Delete(id) 
            ? NoContent() 
            : NotFound();
}
