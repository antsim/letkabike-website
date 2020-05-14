import { UserManager, WebStorageStateStore, User } from 'oidc-client';

export default class AuthService {
    private userManager: UserManager;

    constructor() {
        const STS_DOMAIN: string = 'https://localhost:44356';

        const settings: any = {
            userStore: new WebStorageStateStore({ store: window.localStorage }),
            authority: STS_DOMAIN,
            client_id: 'LetkaBike',
            redirect_uri: 'https://localhost:5001/callback.html',
            automaticSilentRenew: true,
            silent_redirect_uri: 'https://localhost:5001/silent-renew.html',
            response_type: 'code',
            scope: 'openid profile dataEventRecords',
            post_logout_redirect_uri: 'https://localhost:5001/',
            filterProtocolClaims: true,
        };

        this.userManager = new UserManager(settings);
    }

    public getUser(): Promise<User | null> {
        return this.userManager.getUser();
    }

    public login(): Promise<void> {
        return this.userManager.signinRedirect();
    }

    public logout(): Promise<void> {
        return this.userManager.signoutRedirect();
    }

    public getAccessToken(): Promise<string> {
        return this.userManager.getUser().then((data: any) => {
            return data.access_token;
        });
    }
}
