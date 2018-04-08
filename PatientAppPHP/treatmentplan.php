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
                                    <h4 class="title">Treatment Plan for: A zere teen</h4>
                                    <p class="category">Here is your treatment plan for a zere teen.</p>
                                </div>
                                <div class="card-content table-responsive">
                                    <table class="table table-hover">
                                        <thead>
                                            <th width="12%">Step</th>
                                            <th width="30%">Action</th>
                                            <th width="20%">Duration</th>
                                            <th width="18%">End Date</th>
                                            <th style="text-align:center" width="20%">Finished?</th>
                                        </thead>
                                        <tbody>
											<tr>
												<td>1</td>
												<td>Practice stretching your toe</td>
												<td>Two Days</td>
												<td>03-03-2018</td>
												<td style="text-align:center"><input checked type="checkbox" /></td>
											</tr>
											<tr>
												<td>2</td>
												<td>Put a little bit of pressure on your toe</td>
												<td>One Time</td>
												<td>08-03-2018</td>
												<td style="text-align:center"><input type="checkbox" /></td>
											</tr>
											<tr>
												<td>3</td>
												<td>Let mommy put a kiss on it</td>
												<td>As long as you like, you sick individual</td>
												<td>12-03-2018</td>
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
							 <center><input name="save" value="Save Data" type="submit" /></center>
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