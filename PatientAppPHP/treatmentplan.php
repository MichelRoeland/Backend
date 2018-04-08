<!doctype html>
<html lang="en">

<head>
	<?php include "head.php"; error_reporting(-1) ?>
	<title>Treatment Plan &bull; Patient App by StoneyCreek</title>
</head>
<body>
    <div class="wrapper">
        <div class="sidebar" data-color="red" data-image="../assets/img/sidebar-1.jpg">
            <div class="logo">
                <a href="http://www.creative-tim.com" class="simple-text">
                    StoneyCreek
                </a>
            </div>
            <?php include "sidebar.php"; ?>
        </div>
        <div class="main-panel">
            <nav class="navbar navbar-transparent navbar-absolute">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="#">Treatment Plan</a>
                    </div>
                </div>
            </nav>
            <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card card-plain">
                                <div class="card-header" data-background-color="red">
                                    <h4 class="title">Treatment Plan for: Last van uitvallend haar</h4>
                                    <p class="category">The details of your treatment plan for "uitvallend haar"</p>
                                </div>
                                <div class="card-content table-responsive">
                                    <table class="table table-hover">
                                        <thead>
                                            <th width="45%">Type</th>
                                            <th width="45%">Duration</th>
                                            <th style="text-align:center" width="10%">Finished?</th>
                                        </thead>
                                        <tbody>
											<tr>
												<td>Complaints</td>
												<td>Patient has a cold head, unused combs and gel past the expiration date</td>
												<td style="text-align:center"><input checked type="checkbox" /></td>
											</tr>
											<tr>
												<td>Short term solution</td>
												<td>Wig and/or hair piece</td>
												<td style="text-align:center"><input checked type="checkbox" /></td>
											</tr>
											<tr>
												<td>Long term solution</td>
												<td>Patien will grow hair on own strength</td>
												<td style="text-align:center"><input type="checkbox" /></td>
											</tr>
											<tr>
												<td>Treatment 1</td>
												<td>Acceptance and awareness sessions</td>
												<td style="text-align:center"><input type="checkbox" /></td>
											</tr>
											<tr>
												<td>Treatment 2</td>
												<td>Dealing with dissappointment</td>
												<td style="text-align:center"><input type="checkbox" /></td>
											</tr>
											<tr>
												<td>Treatment payed by</td>
												<td>Customer</td>
												<td style="text-align:center"><input type="checkbox" /></td>
											</tr>
											<tr>
												<td>Treatment plan session duration</td>
												<td>45 minutes</td>
												<td style="text-align:center"><input type="checkbox" /></td>
											</tr>
										</tbody>
									</table>
								</div>		
							</div>			
						</div>				
                    </div>
					<div class="row">
						<div class="col-md-12">
							 <center><input name="save" value="Save Progress" type="submit" /></center>
						</div>
					</div>
                </div>
            </div>
            <?php include "footer.php" ?>
        </div>
    </div>
</body>
<!--   Core JS Files   -->
<script src="../assets/js/jquery-3.2.1.min.js" type="text/javascript"></script>
<script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>
<script src="../assets/js/material.min.js" type="text/javascript"></script>
<!--  Charts Plugin -->
<script src="../assets/js/chartist.min.js"></script>
<!--  Dynamic Elements plugin -->
<script src="../assets/js/arrive.min.js"></script>
<!--  PerfectScrollbar Library -->
<script src="../assets/js/perfect-scrollbar.jquery.min.js"></script>
<!--  Notifications Plugin    -->
<script src="../assets/js/bootstrap-notify.js"></script>
<!--  Google Maps Plugin    -->
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=YOUR_KEY_HERE"></script>
<!-- Material Dashboard javascript methods -->
<script src="../assets/js/material-dashboard.js?v=1.2.0"></script>
<!-- Material Dashboard DEMO methods, don't include it in your project! -->
<script src="../assets/js/demo.js"></script>

</html>