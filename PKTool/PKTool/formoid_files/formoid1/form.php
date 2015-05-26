<?php

define('EMAIL_FOR_REPORTS', '');
define('RECAPTCHA_PRIVATE_KEY', '@privatekey@');
define('FINISH_URI', 'http://');
define('FINISH_ACTION', 'message');
define('FINISH_MESSAGE', 'Thanks for filling out my form!');
define('UPLOAD_ALLOWED_FILE_TYPES', 'doc, docx, xls, csv, txt, rtf, html, zip, jpg, jpeg, png, gif');

define('_DIR_', str_replace('\\', '/', dirname(__FILE__)) . '/');
require_once _DIR_ . '/handler.php';

?>

<?php if (frmd_message()): ?>
<link rel="stylesheet" href="<?php echo dirname($form_path); ?>/formoid-metro-cyan.css" type="text/css" />
<span class="alert alert-success"><?php echo FINISH_MESSAGE; ?></span>
<?php else: ?>
<!-- Start Formoid form-->
<link rel="stylesheet" href="<?php echo dirname($form_path); ?>/formoid-metro-cyan.css" type="text/css" />
<script type="text/javascript" src="<?php echo dirname($form_path); ?>/jquery.min.js"></script>
<form class="formoid-metro-cyan" style="background-color:#1A2223;font-size:14px;font-family:'Open Sans','Helvetica Neue','Helvetica',Arial,Verdana,sans-serif;color:#666666;max-width:480px;min-width:150px" method="post"><div class="title"><h2>Pirate Kings</h2></div>
	<div class="element-select<?php frmd_add_class("select"); ?>" title="Account"><label class="title">Account</label><div class="medium"><span><select name="select" >

		<option value="0969023566">0969023566</option>
		<option value="0912901720">0912901720</option>
		<option value="0974260220">0974260220</option>
		<option value="0974260220">0974260220</option>
		<option value="nhothuy48cb@gmail.com">nhothuy48cb@gmail.com</option>
		<option value="lethiminhht@gmail.com">lethiminhht@gmail.com</option></select><i></i></span></div></div>
	<div class="element-checkbox<?php frmd_add_class("checkbox"); ?>"><label class="title"></label>		<div class="column column1"><label><input type="checkbox" name="checkbox[]" value="Attack Random"/ ><span>Attack Random</span></label></div><span class="clearfix"></span>
</div>
	<div class="element-textarea<?php frmd_add_class("textarea"); ?>"><label class="title">Output</label><textarea class="medium" name="textarea" cols="20" rows="5" ></textarea></div>
<div class="submit"><input type="submit" value="OK"/></div></form><script type="text/javascript" src="<?php echo dirname($form_path); ?>/formoid-metro-cyan.js"></script>

<!-- Stop Formoid form-->
<?php endif; ?>

<?php frmd_end_form(); ?>