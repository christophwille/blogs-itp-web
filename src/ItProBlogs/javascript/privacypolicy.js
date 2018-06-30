function sHelp(id){
	var hWidth=440;
	var hHeight=400;
	var hTop=0;
	var hLeft=0;
	var strURL='http://www2.oecd.org/pwv3/';
	var hWindow=window.open(strURL + "help.htm#"+id,"pwhelp","status=yes,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes,width=" + hWidth + ",height=" + hHeight + ",top=" + hTop + ",left=" + hLeft);  
  if (typeof hWindow.focus != "undefined")  
    hWindow.focus();
  }