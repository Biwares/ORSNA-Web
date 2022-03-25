import { Component, OnInit, ViewEncapsulation, Inject, Input } from '@angular/core';
import { DOCUMENT } from '@angular/common';
declare function require(url: string);

export class Constants {

  public static mypath: any = require("../../../angular.json");
  
  private static baseHref: string = Constants.mypath.projects.Orsna.architect.build.options.baseHref;

  public static base: string = Constants.spl(Constants.baseHref);

  public static URL_BASE: string = Constants.base;

  public static URL_BASE_WEB_API: string = Constants.URL_BASE + "/api";

  static readonly DATE_FMT = 'dd/MM/yyyy';

  static readonly DATE_TIME_FMT = `${Constants.DATE_FMT} hh:mm:ss`;
  
  static spl(url: string): string {
    let sp = url.replace(/\//g, "");
    if(sp.length > 0)
      return '/'+sp;
    else
    return "";
  }
}
