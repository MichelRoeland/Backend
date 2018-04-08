<!doctype html>
<html lang="en">

<head>
	<?php include "head.php"; ?>
	<title>Insight Requests &bull; Patient App by StoneyCreek</title>
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
                        <a class="navbar-brand" href="#">Insight Requests</a>
                    </div>
                </div>
            </nav>
            <div class="content">
                <div class="container-fluid">
				<form action="handleform.php" method="POST">
				<?php 
				
				$file = file_get_contents("data/insightrequests.txt");
				
				$split = explode("***", $file); // Rows with insight requests
				
				$pe = 0;
				$ap = 0;
				$de = 0;
				$i = 1;
				$a = 0;
				
				foreach($split as $s){
					$split2 = explode(";", $s);
					
					if($split2[1] == "Pending"){
						$pending[$pe]['doctor'] = $split2[0];
						$pending[$pe]['type'] = $split2[1];						
						$pending[$pe]['datetime'] = $split2[2];	
						$pe++;
					} elseif($split2[1] == "Approved")
					{
						$approved[$ap]['doctor'] = $split2[0];
						$approved[$ap]['type'] = $split2[1];						
						$approved[$ap]['datetime'] = $split2[2];	
						$ap++;
					} elseif($split2[1] == "Denied")
					{
						$denied[$de]['doctor'] = $split2[0];
						$denied[$de]['type'] = $split2[1];						
						$denied[$de]['datetime'] = $split2[2];	
						$de++;
					}
				}
				
				// Creating table for pending
				?>
					<div class="row">
                        <div class="col-md-12">
                            <div class="card card-plain">
                                <div class="card-header" data-background-color="red">
                                    <h4 class="title">Pending Insight Request(s)</h4>
                                    <p class="category">There are people wanting to look into your medical files.</p>
                                </div>
                                <div class="card-content table-responsive">
                                    <table class="table table-hover">
                                        <thead>
                                            <th width="9%">#</th>
                                            <th width="30%">Doctor Name</th>
                                            <th width="30%">Request Date</th>
                                            <th width="30%">Approve/Deny</th>
                                        </thead>
                                        <tbody>
									<?php
										if(count($pending) == 0){
											?>
											<tr>
												<td></td>
												<td>No requests.</td>												
											</tr>
											
											<?php										
										}
										else{
											foreach($pending as $p){
												?>
												<tr>
													<td><?=$i?></td>
													<input type="hidden" name="doctor_<?=$a?>" value="<?=$p['doctor']?>" />
													<td><?=$p['doctor']?></td>
													<input type="hidden" name="datetime_<?=$a?>" value="<?=$p['datetime']?>" />
													<td><?=$p['datetime']?></td>
													<td>
														<select name="permission_<?=$a?>">
															<option value="Pending" selected>-</option>
															<option value="Approved">Approve</option>
															<option value="Denied">Deny</option>
														</select>
													</td>
												</tr>
												<?php
												$i++;
												$a++;
											}
										}										
									?>
										</tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
					<div class="row">
                        <div class="col-md-12">
                            <div class="card card-plain">
                                <div class="card-header" data-background-color="red">
                                    <h4 class="title">Approved Insight Request(s)</h4>
                                    <p class="category">These people are currently able to view your medical files.</p>
                                </div>
                                <div class="card-content table-responsive">
                                    <table class="table table-hover">
                                        <thead>
                                            <th width="9%">#</th>
                                            <th width="30%">Doctor Name</th>
                                            <th width="30%">Request Date</th>
                                            <th width="30%">Approve/Deny</th>
                                        </thead>
                                        <tbody>
									<?php
										if(count($approved) == 0){
											?>
											<tr>
												<td></td>
												<td>No requests.</td>												
											</tr>
											
											<?php										
										}
										else{
												
											$i = 1;
											foreach($approved as $p){
												?>
												<tr>
													<td><?=$i?></td>
													<input type="hidden" name="doctor_<?=$a?>" value="<?=$p['doctor']?>" />
													<td><?=$p['doctor']?></td>
													<input type="hidden" name="datetime_<?=$a?>" value="<?=$p['datetime']?>" />
													<td><?=$p['datetime']?></td>
													<td>
														<select name="permission_<?=$a?>">
															<option value="-" disabled>-</option>
															<option value="Approved" selected>Approve</option>
															<option value="Denied">Deny</option>
														</select>
													</td>
												</tr>
												<?php
												$i++;
												$a++;
											}
										}
									?>
										</tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
					<div class="row">
                        <div class="col-md-12">
                            <div class="card card-plain">
                                <div class="card-header" data-background-color="red">
                                    <h4 class="title">Denied Insight Request(s)</h4>
                                    <p class="category">These people are not able to view your medical files.</p>
                                </div>
                                <div class="card-content table-responsive">
                                    <table class="table table-hover">
                                        <thead>
                                            <th width="9%">#</th>
                                            <th width="30%">Doctor Name</th>
                                            <th width="30%">Request Date</th>
                                            <th width="30%">Approve/Deny</th>
                                        </thead>
                                        <tbody>
									<?php
										if(count($denied) == 0){
											?>
											<tr>
												<td></td>
												<td>No requests.</td>												
											</tr>
											
											<?php										
										}
										else{
											$i = 1;
											foreach($denied as $p){
												?>
												<tr>
													<td><?=$i?></td>
													<input type="hidden" name="doctor_<?=$a?>" value="<?=$p['doctor']?>" />
													<td><?=$p['doctor']?></td>
													<input type="hidden" name="datetime_<?=$a?>" value="<?=$p['datetime']?>" />
													<td><?=$p['datetime']?></td>
													<td>
														<select name="permission_<?=$a?>">
															<option value="-" disabled>-</option>
															<option value="Approved">Approve</option>
															<option value="Denied" selected>Deny</option>
														</select>
													</td>
												</tr>
												<?php
												$i++;
												$a++;
											}
										}
									?>
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
				</form>
            </div>
            <?php include "footer.php"; ?>
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