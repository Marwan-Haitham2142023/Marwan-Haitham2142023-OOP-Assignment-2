using SrpLab;

Console.WriteLine("SrpLab — 10 SRP violations, refactored");
Console.WriteLine("=================================================");

var beds = new WardBedRegistry();
var paging = new WardPaging();
var admissions = new WardAdmissionDesk(new AcuityScorer(), beds, paging);
admissions.Admit(1, "p-88", heartRate: 130, spo2: 89, nowUtc: DateTime.UtcNow);
Console.WriteLine(paging.WriteHandoff(1, beds.Find(1), DateTime.UtcNow));
Console.WriteLine(string.Join(" | ", paging.Drain()));

var cart = new ShoppingCart();
cart.AddLine("SKU-1", 40m, 2);
cart.ApplyCouponText("SAVE10");
cart.EnableGiftWrap();
var price = new CartPricer(new CouponDiscountCalculator()).Price(cart);
var fulfillment = new CheckoutFulfillment();
var auth = fulfillment.Authorize(price.GrandTotal, "4242", cart.GetLines().Count);
Console.WriteLine("basket total=" + price.GrandTotal + " auth=" + auth);

var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);
var priority = new TicketPriorityClassifier().Classify(ticket);
var sla = new SlaPolicy();
var slaDeadline = sla.Deadline(ticket.GetOpenedAt(), priority);
Console.WriteLine(new PublicReplyDrafter().Draft(ticket.GetId(), priority, "Nora", slaDeadline));

var application = new LoanApplication(60_000m, 640, 4, false);
var riskScorer = new LoanRiskScorer();
var risk = riskScorer.Score(application);
var eligible = new LoanEligibilityPolicy(riskScorer).IsEligible(application);
var loanOutputs = new LoanOutputs();
var documents = loanOutputs.BuildChecklist(application, eligible);
Console.WriteLine(loanOutputs.WriteDecisionLetter("Omar", application.RequestedAmount, risk, eligible, documents));

const string courseCode = "SEF-101";
var seating = new CourseSeating(1);
Console.WriteLine(seating.Register("a@mail.com"));
Console.WriteLine(seating.Register("b@mail.com"));
var enrollmentOutputs = new EnrollmentOutputs();
Console.WriteLine(enrollmentOutputs.WriteWelcomePacket(
    courseCode, "Bea", seating.IsSeated("b@mail.com"), seating.WaitlistPosition("b@mail.com")));

var order = new KitchenOrder();
order.AddItem("Pasta", new System.Collections.Generic.List<string> { "wheat", "milk" }, 12);
var allergens = new AllergenDetector().Detect(order.GetItems());
var ops = new KitchenOperations();
var eta = ops.EstimateReadyMinutes(order.GetItems(), 2, allergens.Count > 0);
Console.WriteLine(new ThermalTicketPrinter().Render(42, order.GetItems(), allergens, eta));

var subscription = new Subscription("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
var billing = new BillingOperations();
billing.RegisterFailedPayment();
var amountDue = new ProrationCalculator().Prorate(subscription, subscription.PeriodStart);
var dunningInvoice = billing.NextInvoiceNumber(subscription.PeriodStart);
Console.WriteLine(billing.WriteDunningEmail("Sara", new DateOnly(2026, 9, 20), amountDue, dunningInvoice));

var pickList = new PickList();
pickList.AddNeed("BOLT", "A", 3, 10, 7);
pickList.AddNeed("NUT", "B", 1, 5, 5);
var planning = new WarehousePlanning();
var stops = planning.PlanPath(pickList.GetRequirements());
var allocations = planning.Allocate(pickList.GetRequirements());
Console.WriteLine(new PickerScriptWriter().Write(stops, allocations));

var scores = new StudentScores();
scores.Record("s1", 92);
scores.Record("s1", 88);
var gradingPolicy = new GradingPolicy();
var gradingOutputs = new GradingOutputs(gradingPolicy);
var average = scores.Average("s1");
var letter = gradingPolicy.Letter(average);
Console.WriteLine(gradingOutputs.WriteTranscript("s1", "Ali", average));

var hours = new ClinicOpeningHours(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
var book = new AppointmentBook(hours);
var scheduling = new AppointmentScheduling(hours, book);
var slot = scheduling.FindNextSlot(DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);
if (slot == null) throw new InvalidOperationException("no slot");
book.TryBook(slot.Value);
Console.WriteLine(scheduling.WriteSms(slot.Value, "0100"));


