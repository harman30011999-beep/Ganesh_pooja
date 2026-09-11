<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Ganesh Utsav</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap" rel="stylesheet" />
    <link href="assets/css/landing.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-white shadow-sm fixed-top">
            <div class="container">
                <a class="navbar-brand d-flex align-items-center" href="#">
                    <img src="<%= LandingLogo %>" alt="logo" class="logo-img" />
                    <div>
                        <div class="brand-name"><%= FestivalName %></div>
                        <small class="text-muted"><%= CommitteeName %></small>
                    </div>
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#mainNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="mainNav">
                    <ul class="navbar-nav ms-auto align-items-lg-center gap-lg-3">
                        <li class="nav-item"><a class="nav-link" href="#about">About</a></li>
                        <li class="nav-item"><a class="nav-link" href="#events">Events</a></li>
                        <li class="nav-item"><a class="nav-link" href="#gallery">Gallery</a></li>
                        <li class="nav-item"><a class="nav-link" href="#transparency">Transparency</a></li>
                        <li class="nav-item"><a class="btn btn-primary px-3" href="Login.aspx">Committee Login</a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <header class="hero-section" style="--hero-image: url('<%= LandingBannerImage %>');">
            <div class="container hero-content">
                <div class="row align-items-center">
                    <div class="col-lg-7">
                        <span class="eyebrow">Welcome to <%= FestivalName %></span>
                        <h1>May Lord Ganesha bring joy to every home.</h1>
                        <p class="lead">Wishing you and your family peace, prosperity and a blessed Ganesh Utsav.</p>
                        <div class="hero-actions d-flex flex-wrap gap-3">
                            <% If PublicDonationEnabled Then %>
                                <a href="PublicDonation.aspx" class="btn btn-primary btn-lg">Donate Now</a>
                            <% Else %>
                                <span class="btn btn-secondary btn-lg disabled">Donations Closed</span>
                            <% End If %>
                            <a href="#events" class="btn btn-outline-light btn-lg">View Events</a>
                        </div>
                        <div class="stats-row mt-4 d-flex flex-wrap gap-4">
                            <div>
                                <div class="stat-number"><%= DonationCount %></div>
                                <div class="stat-label">Donations</div>
                            </div>
                            <div>
                                <div class="stat-number"><%= EventCount %></div>
                                <div class="stat-label">Events</div>
                            </div>
                            <div>
                                <div class="stat-number"><%= Year %></div>
                                <div class="stat-label">Festival Year</div>
                            </div>
                        </div>
                        <div class="mt-4 text-white-50">
                            Festival dates: <%= FestivalStartDate %> to <%= FestivalEndDate %>
                        </div>
                    </div>
                    <div class="col-lg-5">
                        <div class="hero-arrival-card">
                            <img src="<%= LandingLogo %>" alt="Ganesh idol" class="hero-idol" />
                            <div id="arrivalTimer" <% If Not ShowArrivalCountdown Then %>style="display:none"<% End If %>>
                                <div class="arrival-label">Ganesh ji arrives in</div>
                                <div id="arrivalCountdown" class="arrival-countdown" data-arrival="<%= ArrivalDateIso %>"><span><strong>0</strong><small>Days</small></span><span><strong>0</strong><small>Hours</small></span><span><strong>0</strong><small>Minutes</small></span><span><strong>0</strong><small>Seconds</small></span></div>
                                <p class="arrival-date">Puja begins <%= ArrivalDateLabel %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <main>
            <section id="about" class="py-5 bg-white">
    <script>
        (function () {
            var countdown = document.getElementById('arrivalCountdown');
            if (!countdown) return;
            var target = new Date(countdown.getAttribute('data-arrival') + 'T00:00:00');
            function update() {
                var remaining = target.getTime() - new Date().getTime();
                var values = remaining > 0 ? [Math.floor(remaining / 86400000), Math.floor(remaining / 3600000) % 24, Math.floor(remaining / 60000) % 60, Math.floor(remaining / 1000) % 60] : [0, 0, 0, 0];
                var units = countdown.querySelectorAll('strong');
                for (var i = 0; i < units.length; i++) units[i].textContent = values[i];
                if (remaining <= 0) document.getElementById('arrivalTimer').style.display = 'none';
            }
            update();
            window.setInterval(update, 1000);
        }());
    </script>
                <div class="container">
                    <div class="row align-items-center g-4">
                        <div class="col-lg-6">
                            <span class="eyebrow text-primary">About the Festival</span>
                            <h2 class="section-title"><%= AboutTitle %></h2>
                            <p class="text-muted"><%= AboutDescription %></p>
                            <ul class="feature-list">
                                <li>Transparent charity and financial updates</li>
                                <li>Community events, bhog and cultural programs</li>
                                <li>Volunteer-driven coordination and event planning</li>
                            </ul>
                        </div>
                        <div class="col-lg-6">
                            <img src="<%= LandingBannerImage %>" alt="Festival" class="img-fluid rounded-4 shadow" />
                        </div>
                    </div>
                </div>
            </section>

            <section id="events" class="py-5 bg-light">
                <div class="container">
                    <div class="d-flex flex-wrap justify-content-between align-items-center mb-4">
                        <div>
                            <span class="eyebrow text-primary">Upcoming</span>
                            <h2 class="section-title">Festival Schedule</h2>
                        </div>
                    </div>
                    <div class="row g-4">
                        <asp:Repeater ID="rptEvents" runat="server">
                            <ItemTemplate>
                                <div class="col-md-6 col-xl-4">
                                    <div class="event-card h-100">
                                        <div class="event-image" style="background-image:url('<%# Eval("PosterUrl") %>');"></div>
                                        <div class="event-body">
                                            <div class="event-date"><%# Eval("EventDate", "{0:dd MMM yyyy}") %></div>
                                            <h4><%# Eval("EventName") %></h4>
                                            <p><%# Eval("Description") %></p>
                                            <div class="meta-row">
                                                <span><%# Eval("Venue") %></span>
                                                <span><%# Eval("EventTime") %></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </section>

            <section id="committee" class="py-5 bg-white">
                <div class="container">
                    <div class="mb-4">
                        <span class="eyebrow text-primary">Trusted Team</span>
                        <h2 class="section-title">Committee Directory</h2>
                    </div>
                    <div class="row g-4">
                        <asp:Repeater ID="rptCommittee" runat="server">
                            <ItemTemplate>
                                <div class="col-md-6 col-xl-4">
                                    <div class="card border-0 shadow-sm h-100">
                                        <div class="card-body">
                                            <div class="d-flex align-items-center mb-3">
                                                <div class="rounded-circle bg-warning-subtle text-warning fw-bold d-flex align-items-center justify-content-center me-3" style:"width:48px;height:48px;">
                                                    <%# Left(Eval("FullName").ToString(), 1).ToUpper() %>
                                                </div>
                                                <div>
                                                    <h5 class="mb-0"><%# Eval("FullName") %></h5>
                                                    <small class="text-muted"><%# Eval("Position") %></small>
                                                </div>
                                            </div>
                                            <p class="mb-1"><strong>Phone:</strong> <%# Eval("Phone") %></p>
                                            <p class="mb-0"><strong>Email:</strong> <%# Eval("Email") %></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </section>

            <section class="py-5 bg-light">
                <div class="container">
                    <div class="row align-items-center g-4">
                        <div class="col-lg-6">
                            <span class="eyebrow text-primary">Support</span>
                            <h2 class="section-title">Donation & Sponsorship Portal</h2>
                            <p class="text-muted">Your seva and generosity help organize a peaceful and memorable celebration for the entire community.</p>
                            <div class="feature-list">
                                <div class="mb-3"><strong>UPI ID:</strong> <%= PublicUpiId %></div>
                                <div class="mb-3"><strong>Festival Dates:</strong> <%= FestivalStartDate %> to <%= FestivalEndDate %></div>
                                <div class="mb-3"><strong>Current Balance:</strong> Rs. <%=(CurrentBalance) %></div>
                            </div>
                            <% If PublicDonationEnabled Then %>
                                <a href="PublicDonation.aspx" class="btn btn-primary btn-lg">Donate Now</a>
                            <% End If %>
                        </div>
                        <div class="col-lg-6">
                            <div class="card border-0 shadow-sm h-100">
                                <div class="card-body p-4">
                                    <h4 class="mb-3">Public Donation Details</h4>
                                    <div class="alert alert-warning">Please scan the UPI QR or use the public donation form to support the puja committee.</div>
                                    <div class="bg-light rounded p-3 text-center">
                                        <div class="display-6 mb-2">UPI</div>
                                        <div class="fw-bold"><%= PublicUpiId %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section id="gallery" class="py-5 bg-white">
                <div class="container">
                    <div class="mb-4">
                        <span class="eyebrow text-primary">Memories</span>
                        <h2 class="section-title">Photo Gallery</h2>
                    </div>
                    <div class="row g-4">
                        <asp:Repeater ID="rptGallery" runat="server">
                            <ItemTemplate>
                                <div class="col-md-6 col-xl-4">
                                    <div class="gallery-card h-100">
                                        <div class="gallery-cover" style="background-image:url('<%# Eval("CoverImage") %>');"></div>
                                        <div class="gallery-body">
                                            <h4><%# Eval("AlbumTitle") %></h4>
                                            <div class="meta-row">
                                                <span><%# Eval("Visibility") %></span>
                                                <span><%# Eval("FestivalYear") %></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </section>

            <section id="transparency" class="py-5 bg-light">
                <div class="container">
                    <div class="text-center mb-4">
                        <span class="eyebrow text-primary">Open Book</span>
                        <h2 class="section-title">Festival Transparency</h2>
                    </div>
                    <div class="row g-4">
                        <div class="col-md-6 col-xl-3">
                            <div class="metric-card">
                                <label>Opening Balance</label>
                                <strong>Rs. <%=(OpenBalance) %></strong>
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="metric-card success">
                                <label>Confirmed Donations</label>
                                <strong>Rs. <%=(ConfirmedDonationTotal) %></strong>
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="metric-card danger">
                                <label>Approved Expenses</label>
                                <strong>Rs.<%= (ApprovedExpenseTotal) %></strong>
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="metric-card warning">
                                <label>Current Balance</label>
                                <strong>Rs. <%= (CurrentBalance) %></strong>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </main>

        <footer class="footer">
            <div class="container d-flex flex-column flex-md-row justify-content-between align-items-center">
                <div>
                    <strong><%= FestivalName %></strong><br />
                    <small><%= CommitteeName %></small>
                </div>
                <div class="small text-light mt-2 mt-md-0">© <%= Year %> • All rights reserved</div>
            </div>
        </footer>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
