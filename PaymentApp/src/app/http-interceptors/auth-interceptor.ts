
import { HttpInterceptorFn } from '@angular/common/http';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {



  const authReq = req.clone({
    headers: req.headers.set('X-API-KEY', 'QXBpS2V5TWlkZGxld2FyZQ==')
  });

  return next(authReq);
};
