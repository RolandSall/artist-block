import crypto from 'crypto';

// eslint-disable-next-line import/prefer-default-export
export const hashIds = (ids) => {
    if (!ids) {
        return;
    }
    crypto
        .createHash('sha1')
        .update(ids.sort().join(''))
        .digest('hex')
        .toString();
};
