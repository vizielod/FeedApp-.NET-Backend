//
//
// POST: /Dinners/Create

[AcceptVerbs(HttpVerbs.Post)]
public ActionResult Create(Dinner dinner) {

    if (ModelState.IsValid) {

        try {
            dinner.HostedBy = "SomeUser";

            dinnerRepository.Add(dinner);
            dinnerRepository.Save();

            return RedirectToAction("Details", new {id = dinner.DinnerID });
        }
        catch {        
            ModelState.AddRuleViolations(dinner.GetRuleViolations());
        }
    }
    
    return View(dinner);
}