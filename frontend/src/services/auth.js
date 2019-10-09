import request from '../utils/request';

const login = params => {
  return request({
    url: '/Users/authenticate',
    method: 'POST',
    data: params
  });
};

export { login };
