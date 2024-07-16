import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44387/',
  redirectUri: baseUrl,
  clientId: 'AuthMobile_App',
  responseType: 'code',
  scope: 'offline_access AuthMobile',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'AuthMobile',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44387',
      rootNamespace: 'AuthMobile',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
