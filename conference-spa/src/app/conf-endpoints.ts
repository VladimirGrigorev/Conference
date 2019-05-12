export const DOMAIN = 'http://localhost:5000/api';

const AUTH_ENDPOINT = DOMAIN + 'auth';
export const SIGNIN_ENDPOINT = AUTH_ENDPOINT + '/signin';
export const SIGNUP_ENDPOINT = AUTH_ENDPOINT + '/signup';

export const CONFERENCES_ENDPOINT = DOMAIN + '/conferences';
export const LECTURES_ENDPOINT = DOMAIN + '/lectures';
export const SECTIONS_ENDPOINT = DOMAIN + '/section';

export const FILES_ENDPOINT = DOMAIN + '/files';
export const FORUM_ENDPOINT = DOMAIN + '/message';
export const USER_ENDPOINT = DOMAIN + '/user';
export const SCHEDULE_ENDPOINT = DOMAIN + '/my/lectures';

export const APPLICATION_ENDPOINT = DOMAIN + '/application';