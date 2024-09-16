import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './ApiServices/auth.service';
import { inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService); // Using `inject` to access services
  const router = inject(Router);
  const platformId = inject(PLATFORM_ID);

  if (isPlatformBrowser(platformId) && authService.isAuthenticated()) {
    return true;
  } else {
    router.navigate(['/login']); // Redirect to login if not authenticated
    return false;
  }
};

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService); // Using `inject` to access services
  const router = inject(Router);
  const platformId = inject(PLATFORM_ID);
  const requiredRole = route.data?.['requiredRole']; // Access route data to get the required role

  if (isPlatformBrowser(platformId) && authService.hasRole(requiredRole)) {
    return true;
  } else {
    router.navigate(['/forbidden']); // Redirect to forbidden if the role doesn't match
    return false;
  }
};
