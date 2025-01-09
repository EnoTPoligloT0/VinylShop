import {NextResponse} from 'next/server';
import type {NextRequest} from 'next/server';
import {jwtDecode} from "jwt-decode";
import {JwtPayload} from "@/types/jwtPayload";

export function middleware(req: NextRequest) {
    const token = req.cookies.get('secretCookie')?.value;

    if (!token) {
        return NextResponse.redirect(new URL('/login', req.url));
    }


    try {
        const payload: JwtPayload = jwtDecode(token);

        console.log(payload);

        if (payload.Role !== 'Admin') {
            return NextResponse.redirect(new URL('/', req.url));
        }

        return NextResponse.next();
    } catch (error) {
        console.error('Invalid JWT:', error);
        return NextResponse.redirect(new URL('/login', req.url));
    }
}

export const config = {
    matcher: ['/admin/:path*', '/*'],
};

