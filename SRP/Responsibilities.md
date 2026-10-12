
## The 10 Domains

### Ward
Old class: WardManager
Hospital ward.
Patient score, bed assignment, pager, handoff note, census CSV.

### Checkout
Old class: CheckoutService
Online cart.
Items, coupons, gift wrap, price, payment.

### Support
Old class: TicketService
Support tickets.
Priority, SLA, public reply, escalation note.

### Loans
Old class: LoanProcessor
Bank loans.
Risk score, eligibility, documents, decision letter.

### Enrollment
Old class: EnrollmentService
Course sign-up.
Seats, waitlist, welcome packet, invoice.

### Kitchen
Old class: KitchenService
Restaurant.
Order, allergens, cook time, ticket, expo lane.

### Billing
Old class: BillingService
Subscriptions.
Proration, invoice number, failed payments, dunning email, ledger.

### Warehouse
Old class: WarehouseService
Picking.
Allocate stock, plan path, picker script, WMS XML.

### Grading
Old class: GradeBook
Scores.
Average, letter grade, honor roll, transcript, CSV.

### Appointments
Old class: AppointmentService
Clinic.
Opening hours, booking, slot finder, ICS, SMS.

## The Split Classes

### Ward
BedAssignment - bed, patient, acuity
AcuityScorer - compute score from heart rate and SpO2
WardBedRegistry - which patient is in which bed
WardPaging - pager code, log, handoff note, census CSV
WardAdmissionDesk - runs the admission steps

### Checkout
BasketLine - one item line in the cart
CartPrice - subtotal, discount, gift wrap, grand total
ShoppingCart - holds lines, coupon, gift wrap flag
CouponDiscountCalculator - discount from coupon text
CartPricer - combines subtotal, discount, and fees
CheckoutFulfillment - gift card and payment authorization

### Support
TicketPriority - enum P1, P2, P3
SupportTicket - ticket data
TicketPriorityClassifier - picks priority from keywords
SlaPolicy - deadline by priority plus escalation note
PublicReplyDrafter - writes the public reply

### Loans
LoanApplication - loan request data
LoanRiskScorer - risk formula
LoanEligibilityPolicy - approve or reject
LoanOutputs - document list, decision letter, CSV row

### Enrollment
CourseSeating - seats, waitlist, promote
EnrollmentOutputs - welcome packet and invoice line

### Kitchen
KitchenOrderItem - dish, ingredients, prep time
KitchenOrder - list of dishes
AllergenDetector - finds allergens
KitchenOperations - cook time and lane choice
ThermalTicketPrinter - prints the kitchen ticket

### Billing
Subscription - customer, price, period
ProrationCalculator - partial period amount
BillingOperations - invoice number, failed count, dunning, ledger

### Warehouse
PickRequirement - what to pick
StockAllocation - how much we can give
PickStop - one stop on the path
PickList - holds requirements
WarehousePlanning - allocate stock and plan the path
PickerScriptWriter - text for the handheld device
WmsXmlBatchWriter - XML for the WMS

### Grading
StudentScores - stores scores, computes average
GradingPolicy - letter grade and honor roll
GradingOutputs - transcript and CSV

### Appointments
ClinicOpeningHours - open, close, slot length
AppointmentBook - books and checks slots
AppointmentScheduling - finds a slot, writes ICS and SMS

