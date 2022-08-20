import { StatusCodes } from 'http-status-codes';

export const MEMBER_NOT_FOUND_ERROR = 'MEMBER_NOT_FOUND_ERROR';

export class UserIsSignedOut extends Error {
    code;
    constructor() {
        super('User is not authenticated');
        this.code = StatusCodes.UNAUTHORIZED;
    }
}
