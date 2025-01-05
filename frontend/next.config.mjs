import withBundleAnalyzer from '@next/bundle-analyzer';

const nextConfig = withBundleAnalyzer({
    enabled: process.env.ANALYZE === 'true',
})({

    reactStrictMode: true,
});

export default nextConfig;