"use strict"


var canvasErros = document.getElementById("canvas-erros");
var ctxErros = canvasErros.getContext("2d");
var canvasWarinngs = document.getElementById("canvas-warinngs");
var ctxWarinngs = canvasWarinngs.getContext("2d");

var apps = [{"Nome" : "FakeBook", "Erros" : 30, "Warnings" : 50}, {"Nome" : "Xml Reader", "Erros" : 20, "Warnings" : 60}]

function desenhaLinhas(ctx){
  ctx.beginPath();
  ctx.lineTo(280, 130);
  ctx.lineTo(50,130);
  ctx.lineTo(50,20);
  ctx.lineTo(50, 130);
  ctx.stroke();
  ctx.closePath();

  for(var i=1; i<=8; i++){
    ctx.fillText(i+"0", 35, (130-(i*15)));
  }

}

function desenhaNomes(appsList, ctx){
  appsList.forEach(function(app, index){
    ctx.fillText(app.Nome, (index+1)*60, 140);
  });
}

function desenhaBarras(appsList, ctx, tipo){
  appsList.forEach(function(app, index){
    ctx.fillRect((index+1)*70, 130-(app[tipo]*1.5), 20, (app[tipo]*1.5));
  });
}

function desenhaGrafico(ctx, cor, texto){
  ctx.fillStyle = cor;
  ctx.fillText(texto, 230, 10);
  desenhaLinhas(ctx);
  desenhaNomes(apps, ctx);
  desenhaBarras(apps, ctx, texto);
  desenhaBarras(apps, ctx, texto);
}

desenhaGrafico(ctxErros, "#FF0000", "Erros");
desenhaGrafico(ctxWarinngs, "#FFC852", "Warnings");
