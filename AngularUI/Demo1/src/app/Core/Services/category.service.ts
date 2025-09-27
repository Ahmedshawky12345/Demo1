import { Injectable } from "@angular/core";
import { environment } from "../../../Environments/environment";
import { HttpClient } from "@angular/common/http";
import { PagedResponse } from "../Models/paged-response.model";
import { Category } from "../Models/category.model";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'  
  })
  export class CategoryService{
private Baseurl=environment.apiUrl
constructor(private http:HttpClient ){}

GetCategory(pageNumber:number,pageSize:number):Observable<any>{
    return this.http.get<PagedResponse<Category>>(`${this.Baseurl}/Category/GetCategory?pagenumber=${pageNumber}&pagesize=${pageSize}`);

}
  }
