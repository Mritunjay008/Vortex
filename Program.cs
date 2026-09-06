using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
var app = builder.Build();

app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>VORTEX X-1 | Ultra-Light Precision Gaming Mouse</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&family=JetBrains+Mono:wght@400;500;700&display=swap" rel="stylesheet">
    <style>
        :root {
            --bg-base: #090c10;
            --bg-card: #0f151f;
            --bg-card-hover: #162030;
            --accent-cyan: #00f2fe;
            --accent-glow: rgba(0, 242, 254, 0.35);
            --accent-purple: #8e2de2;
            --text-primary: #f0f6fc;
            --text-secondary: #8b949e;
            --border-subtle: rgba(255, 255, 255, 0.08);
            --border-hover: rgba(0, 242, 254, 0.4);
            --transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
        }

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Plus Jakarta Sans', -apple-system, sans-serif;
            background-color: var(--bg-base);
            color: var(--text-primary);
            line-height: 1.6;
            overflow-x: hidden;
            selection-background-color: var(--accent-cyan);
        }

        /* Ambient background glow */
        .ambient-glow {
            position: fixed;
            top: -200px;
            left: 50%;
            transform: translateX(-50%);
            width: 900px;
            height: 600px;
            background: radial-gradient(circle, rgba(0, 242, 254, 0.12) 0%, rgba(142, 45, 226, 0.08) 50%, transparent 70%);
            pointer-events: none;
            z-index: 0;
            filter: blur(80px);
        }

        /* Navigation */
        nav {
            position: sticky;
            top: 0;
            background: rgba(9, 12, 16, 0.85);
            backdrop-filter: blur(16px);
            border-bottom: 1px solid var(--border-subtle);
            z-index: 100;
            padding: 1rem 2rem;
        }

        .nav-container {
            max-width: 1200px;
            margin: 0 auto;
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .logo {
            display: flex;
            align-items: center;
            gap: 0.6rem;
            font-weight: 800;
            font-size: 1.3rem;
            letter-spacing: 0.5px;
            text-decoration: none;
            color: var(--text-primary);
        }

        .logo-badge {
            width: 28px;
            height: 28px;
            background: linear-gradient(135deg, var(--accent-cyan), var(--accent-purple));
            border-radius: 6px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: 800;
            color: #000;
            font-size: 0.9rem;
        }

        .nav-links {
            display: flex;
            gap: 2rem;
            list-style: none;
        }

        .nav-links a {
            color: var(--text-secondary);
            text-decoration: none;
            font-weight: 500;
            font-size: 0.95rem;
            transition: var(--transition);
        }

        .nav-links a:hover {
            color: var(--accent-cyan);
        }

        .nav-actions {
            display: flex;
            align-items: center;
            gap: 1rem;
        }

        .btn-order {
            background: linear-gradient(135deg, var(--accent-cyan), #00c0fa);
            color: #050a12;
            padding: 0.6rem 1.4rem;
            border-radius: 9999px;
            font-weight: 700;
            font-size: 0.9rem;
            text-decoration: none;
            border: none;
            cursor: pointer;
            box-shadow: 0 4px 15px var(--accent-glow);
            transition: var(--transition);
        }

        .btn-order:hover {
            transform: translateY(-2px);
            box-shadow: 0 6px 25px rgba(0, 242, 254, 0.5);
        }

        /* Hero Section */
        .hero {
            position: relative;
            max-width: 1200px;
            margin: 0 auto;
            padding: 5rem 2rem 4rem;
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 3rem;
            align-items: center;
            z-index: 1;
        }

        .hero-tag {
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
            background: rgba(0, 242, 254, 0.1);
            border: 1px solid rgba(0, 242, 254, 0.25);
            padding: 0.35rem 0.85rem;
            border-radius: 9999px;
            font-size: 0.8rem;
            font-weight: 600;
            color: var(--accent-cyan);
            margin-bottom: 1.5rem;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-family: 'JetBrains Mono', monospace;
        }

        .hero-tag span.dot {
            width: 7px;
            height: 7px;
            background: var(--accent-cyan);
            border-radius: 50%;
            box-shadow: 0 0 10px var(--accent-cyan);
        }

        .hero-title {
            font-size: 3.5rem;
            font-weight: 800;
            line-height: 1.1;
            letter-spacing: -1.5px;
            margin-bottom: 1.5rem;
        }

        .hero-title .gradient-text {
            background: linear-gradient(135deg, #ffffff 30%, var(--accent-cyan) 80%, var(--accent-purple) 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .hero-desc {
            color: var(--text-secondary);
            font-size: 1.15rem;
            margin-bottom: 2rem;
            max-width: 500px;
        }

        .hero-cta {
            display: flex;
            gap: 1rem;
            align-items: center;
            margin-bottom: 3rem;
        }

        .btn-secondary {
            background: rgba(255, 255, 255, 0.05);
            color: var(--text-primary);
            border: 1px solid var(--border-subtle);
            padding: 0.65rem 1.4rem;
            border-radius: 9999px;
            font-weight: 600;
            font-size: 0.95rem;
            text-decoration: none;
            cursor: pointer;
            transition: var(--transition);
        }

        .btn-secondary:hover {
            background: rgba(255, 255, 255, 0.1);
            border-color: rgba(255, 255, 255, 0.2);
        }

        .hero-metrics {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 1.5rem;
            border-top: 1px solid var(--border-subtle);
            padding-top: 2rem;
        }

        .metric-val {
            font-size: 1.8rem;
            font-weight: 800;
            color: #fff;
            font-family: 'JetBrains Mono', monospace;
        }

        .metric-label {
            font-size: 0.8rem;
            color: var(--text-secondary);
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        /* Mouse Visual Showcase */
        .mouse-stage {
            position: relative;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
        }

        .mouse-chassis {
            position: relative;
            width: 320px;
            height: 480px;
            background: radial-gradient(circle at 50% 30%, #1c2635 0%, #0d121a 70%);
            border-radius: 130px 130px 100px 100px;
            border: 2px solid rgba(255, 255, 255, 0.08);
            box-shadow: 0 25px 60px rgba(0, 0, 0, 0.6), 0 0 50px var(--accent-glow);
            transition: var(--transition);
            display: flex;
            flex-direction: column;
            align-items: center;
            padding-top: 20px;
            overflow: hidden;
        }

        /* Split mouse buttons */
        .button-split {
            position: absolute;
            top: 0;
            left: 50%;
            width: 2px;
            height: 170px;
            background: rgba(0, 0, 0, 0.6);
            box-shadow: 1px 0 0 rgba(255, 255, 255, 0.05);
        }

        .scroll-wheel {
            position: relative;
            width: 24px;
            height: 60px;
            background: #080a0f;
            border: 2px solid var(--accent-cyan);
            border-radius: 12px;
            margin-top: 30px;
            box-shadow: 0 0 15px var(--accent-glow);
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .scroll-ribs {
            width: 14px;
            height: 3px;
            background: var(--accent-cyan);
            box-shadow: 0 8px 0 var(--accent-cyan), 0 -8px 0 var(--accent-cyan);
        }

        .dpi-switch {
            width: 12px;
            height: 18px;
            background: #111822;
            border: 1px solid rgba(255, 255, 255, 0.2);
            border-radius: 4px;
            margin-top: 15px;
        }

        .rgb-crest {
            position: absolute;
            bottom: 50px;
            width: 48px;
            height: 48px;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .rgb-crest svg {
            width: 38px;
            height: 38px;
            fill: none;
            stroke: var(--accent-cyan);
            stroke-width: 2;
            filter: drop-shadow(0 0 8px var(--accent-cyan));
        }

        .rgb-underglow {
            position: absolute;
            bottom: 8px;
            width: 160px;
            height: 4px;
            background: var(--accent-cyan);
            border-radius: 999px;
            box-shadow: 0 0 25px 5px var(--accent-cyan);
        }

        /* Color customizer controls */
        .color-selector {
            margin-top: 2rem;
            display: flex;
            align-items: center;
            gap: 1rem;
            background: var(--bg-card);
            border: 1px solid var(--border-subtle);
            padding: 0.6rem 1.2rem;
            border-radius: 9999px;
        }

        .color-label {
            font-size: 0.85rem;
            color: var(--text-secondary);
            font-weight: 600;
        }

        .color-dot {
            width: 22px;
            height: 22px;
            border-radius: 50%;
            cursor: pointer;
            border: 2px solid transparent;
            transition: var(--transition);
        }

        .color-dot.active {
            border-color: #fff;
            transform: scale(1.15);
        }

        .dot-stealth { background: #1a222e; }
        .dot-polar { background: #e5e9f0; }
        .dot-cyber { background: linear-gradient(135deg, #00f2fe, #8e2de2); }

        /* Feature section */
        .features {
            max-width: 1200px;
            margin: 4rem auto;
            padding: 0 2rem;
            z-index: 1;
            position: relative;
        }

        .section-header {
            text-align: center;
            max-width: 600px;
            margin: 0 auto 3.5rem;
        }

        .section-header h2 {
            font-size: 2.2rem;
            font-weight: 800;
            margin-bottom: 0.8rem;
            letter-spacing: -0.5px;
        }

        .section-header p {
            color: var(--text-secondary);
        }

        .features-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 1.5rem;
        }

        .feature-card {
            background: var(--bg-card);
            border: 1px solid var(--border-subtle);
            border-radius: 16px;
            padding: 2rem;
            transition: var(--transition);
            position: relative;
            overflow: hidden;
        }

        .feature-card:hover {
            transform: translateY(-5px);
            border-color: var(--border-hover);
            background: var(--bg-card-hover);
        }

        .feature-icon {
            width: 44px;
            height: 44px;
            background: rgba(0, 242, 254, 0.1);
            border: 1px solid rgba(0, 242, 254, 0.2);
            border-radius: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.3rem;
            margin-bottom: 1.2rem;
        }

        .feature-card h3 {
            font-size: 1.2rem;
            font-weight: 700;
            margin-bottom: 0.6rem;
        }

        .feature-card p {
            color: var(--text-secondary);
            font-size: 0.95rem;
            line-height: 1.5;
        }

        /* Interactive DPI Simulator */
        .dpi-section {
            max-width: 1200px;
            margin: 5rem auto;
            padding: 3rem 2rem;
            background: linear-gradient(180deg, var(--bg-card) 0%, #0a0e17 100%);
            border: 1px solid var(--border-subtle);
            border-radius: 24px;
            text-align: center;
        }

        .dpi-controls {
            max-width: 500px;
            margin: 2rem auto 1rem;
        }

        .dpi-slider {
            width: 100%;
            -webkit-appearance: none;
            height: 8px;
            border-radius: 5px;
            background: #1c2635;
            outline: none;
            cursor: pointer;
        }

        .dpi-slider::-webkit-slider-thumb {
            -webkit-appearance: none;
            appearance: none;
            width: 22px;
            height: 22px;
            border-radius: 50%;
            background: var(--accent-cyan);
            cursor: pointer;
            box-shadow: 0 0 12px var(--accent-cyan);
        }

        .dpi-display {
            font-family: 'JetBrains Mono', monospace;
            font-size: 2.8rem;
            font-weight: 800;
            color: var(--accent-cyan);
            margin: 1rem 0;
            text-shadow: 0 0 20px var(--accent-glow);
        }

        /* Tech Specs comparison */
        .specs-table {
            width: 100%;
            max-width: 800px;
            margin: 2rem auto;
            border-collapse: collapse;
            text-align: left;
        }

        .specs-table tr {
            border-bottom: 1px solid var(--border-subtle);
        }

        .specs-table td {
            padding: 1rem 1.5rem;
            font-size: 0.95rem;
        }

        .specs-table td.spec-title {
            color: var(--text-secondary);
            font-weight: 500;
            width: 35%;
        }

        .specs-table td.spec-val {
            color: #fff;
            font-weight: 600;
            font-family: 'JetBrains Mono', monospace;
        }

        /* Pre-order banner */
        .cta-banner {
            max-width: 1200px;
            margin: 4rem auto 6rem;
            padding: 4rem 2rem;
            border-radius: 24px;
            background: linear-gradient(135deg, rgba(0, 242, 254, 0.12) 0%, rgba(142, 45, 226, 0.15) 100%);
            border: 1px solid rgba(0, 242, 254, 0.25);
            text-align: center;
        }

        .cta-banner h2 {
            font-size: 2.5rem;
            font-weight: 800;
            margin-bottom: 1rem;
        }

        .cta-banner p {
            color: var(--text-secondary);
            max-width: 540px;
            margin: 0 auto 2rem;
            font-size: 1.1rem;
        }

        /* Toast notification */
        .toast {
            position: fixed;
            bottom: 2rem;
            right: 2rem;
            background: #111a28;
            border: 1px solid var(--accent-cyan);
            box-shadow: 0 10px 30px rgba(0, 242, 254, 0.3);
            color: #fff;
            padding: 1rem 1.5rem;
            border-radius: 12px;
            display: none;
            align-items: center;
            gap: 0.8rem;
            z-index: 1000;
            animation: slideIn 0.3s cubic-bezier(0.16, 1, 0.3, 1);
        }

        @keyframes slideIn {
            from { transform: translateY(20px); opacity: 0; }
            to { transform: translateY(0); opacity: 1; }
        }

        /* Footer */
        footer {
            border-top: 1px solid var(--border-subtle);
            padding: 3rem 2rem;
            text-align: center;
            color: var(--text-secondary);
            font-size: 0.9rem;
        }

        .footer-badge {
            display: inline-flex;
            align-items: center;
            gap: 0.4rem;
            background: rgba(255, 255, 255, 0.05);
            border: 1px solid var(--border-subtle);
            padding: 0.3rem 0.8rem;
            border-radius: 6px;
            font-family: 'JetBrains Mono', monospace;
            font-size: 0.8rem;
            color: var(--accent-cyan);
            margin-top: 1rem;
        }

        /* Responsive */
        @media (max-width: 868px) {
            .hero { grid-template-columns: 1fr; text-align: center; padding-top: 2rem; }
            .hero-desc { margin: 0 auto 2rem; }
            .hero-cta { justify-content: center; }
            .hero-metrics { justify-content: center; }
            .features-grid { grid-template-columns: 1fr; }
            .nav-links { display: none; }
        }
    </style>
</head>
<body>

    <div class="ambient-glow"></div>

    <!-- Navigation -->
    <nav>
        <div class="nav-container">
            <a href="/" class="logo">
                <div class="logo-badge">V</div>
                <span>VORTEX</span>
            </a>
            <ul class="nav-links">
                <li><a href="#overview">Overview</a></li>
                <li><a href="#features">Technology</a></li>
                <li><a href="#dpi">Sensor Simulator</a></li>
                <li><a href="#specs">Specifications</a></li>
            </ul>
            <div class="nav-actions">
                <button class="btn-order" onclick="triggerCart()">Pre-Order $89</button>
            </div>
        </div>
    </nav>

    <!-- Hero Section -->
    <section class="hero" id="overview">
        <div class="hero-content">
            <div class="hero-tag">
                <span class="dot"></span> Next-Gen Wireless Flagship
            </div>
            <h1 class="hero-title">
                Engineered for Instinct.<br>
                <span class="gradient-text">VORTEX X-1.</span>
            </h1>
            <p class="hero-desc">
                49 grams of pure optical mastery. Equipped with our proprietary 32,000 DPI sensor, true 8,000Hz polling rate, and 90 hours of zero-latency wireless gameplay.
            </p>
            <div class="hero-cta">
                <button class="btn-order" onclick="triggerCart()">Order Now &bull; $89</button>
                <a href="#specs" class="btn-secondary">Technical Specs</a>
            </div>
            <div class="hero-metrics">
                <div>
                    <div class="metric-val">49g</div>
                    <div class="metric-label">Featherweight</div>
                </div>
                <div>
                    <div class="metric-val">32K</div>
                    <div class="metric-label">Max DPI</div>
                </div>
                <div>
                    <div class="metric-val">8000Hz</div>
                    <div class="metric-label">Polling Rate</div>
                </div>
            </div>
        </div>

        <div class="mouse-stage">
            <div class="mouse-chassis" id="mouseChassis">
                <div class="button-split"></div>
                <div class="scroll-wheel" id="scrollWheel">
                    <div class="scroll-ribs" id="scrollRibs"></div>
                </div>
                <div class="dpi-switch"></div>
                
                <div class="rgb-crest">
                    <svg viewBox="0 0 24 24">
                        <polygon points="12 2 2 7 12 12 22 7 12 2"></polygon>
                        <polyline points="2 17 12 22 22 17"></polyline>
                        <polyline points="2 12 12 17 22 12"></polyline>
                    </svg>
                </div>
                <div class="rgb-underglow" id="rgbUnderglow"></div>
            </div>

            <!-- Color customizer -->
            <div class="color-selector">
                <span class="color-label">Colorway:</span>
                <div class="color-dot dot-stealth active" title="Stealth Shadow" onclick="setMouseColor('stealth', this)"></div>
                <div class="color-dot dot-polar" title="Glacier White" onclick="setMouseColor('polar', this)"></div>
                <div class="color-dot dot-cyber" title="Cyber Pulse" onclick="setMouseColor('cyber', this)"></div>
            </div>
        </div>
    </section>

    <!-- Features -->
    <section class="features" id="features">
        <div class="section-header">
            <h2>Dominance in Every Millisecond</h2>
            <p>Every component stripped of excess weight, reinforced with aerospace-grade composite alloy.</p>
        </div>

        <div class="features-grid">
            <div class="feature-card">
                <div class="feature-icon">
                    <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polygon points="13 2 3 14 12 14 11 22 21 10 12 10 13 2"/></svg>
                </div>
                <h3>HyperSpeed 8,000Hz</h3>
                <p>Delivers up to 8 times faster reporting speed than standard 1000Hz gaming mice, eliminating micro-stutters and input lag down to 0.125ms.</p>
            </div>
            <div class="feature-card">
                <div class="feature-icon">
                    <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><line x1="22" y1="12" x2="18" y2="12"/><line x1="6" y1="12" x2="2" y2="12"/><line x1="12" y1="6" x2="12" y2="2"/><line x1="12" y1="22" x2="12" y2="18"/></svg>
                </div>
                <h3>Pro-Optic 32,000 Sensor</h3>
                <p>Pixel-perfect tracking on any surface, including glass. Features auto-surface calibration and asymmetric cut-off lift-off customization.</p>
            </div>
            <div class="feature-card">
                <div class="feature-icon">
                    <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/></svg>
                </div>
                <h3>Gen-4 Optical Switches</h3>
                <p>Infrared light beam actuation provides instant 0.2ms trigger response with zero debounce delay and a certified 100-million click lifespan.</p>
            </div>
        </div>
    </section>

    <!-- Interactive DPI Simulator -->
    <section class="dpi-section" id="dpi">
        <div class="section-header">
            <h2>Instant Sensitivity Control</h2>
            <p>Slide to preview the on-the-fly hardware DPI switching curve.</p>
        </div>
        <div class="dpi-display" id="dpiValue">3,200 DPI</div>
        <div class="dpi-controls">
            <input type="range" min="400" max="32000" step="400" value="3200" class="dpi-slider" id="dpiSlider" oninput="updateDpi(this.value)">
        </div>
        <p style="color: var(--text-secondary); font-size: 0.9rem;" id="dpiNote">Ideal for: High precision esports FPS & tactical tracking.</p>
    </section>

    <!-- Specs Section -->
    <section class="features" id="specs">
        <div class="section-header">
            <h2>Technical Specifications</h2>
            <p>Engineered to the millimeter for competitive ergonomics.</p>
        </div>

        <table class="specs-table">
            <tr>
                <td class="spec-title">Weight</td>
                <td class="spec-val">49 grams (&plusmn;1g)</td>
            </tr>
            <tr>
                <td class="spec-title">Sensor</td>
                <td class="spec-val">VORTEX Pro-Optic 32K Optical</td>
            </tr>
            <tr>
                <td class="spec-title">Polling Rate</td>
                <td class="spec-val">Up to 8,000 Hz (Wireless & Wired)</td>
            </tr>
            <tr>
                <td class="spec-title">Max Acceleration</td>
                <td class="spec-val">70 G / 750 IPS tracking speed</td>
            </tr>
            <tr>
                <td class="spec-title">Battery Life</td>
                <td class="spec-val">Up to 90 hours @ 1000Hz / 25 hours @ 8000Hz</td>
            </tr>
            <tr>
                <td class="spec-title">Connectivity</td>
                <td class="spec-val">2.4GHz HyperSpeed, Bluetooth 5.3, USB-C Paracord</td>
            </tr>
            <tr>
                <td class="spec-title">Skates</td>
                <td class="spec-val">100% Virgin Grade Curved PTFE Glides</td>
            </tr>
        </table>
    </section>

    <!-- Call to action -->
    <section class="cta-banner">
        <h2>Ready to Upgrade Your Precision?</h2>
        <p>Order the VORTEX X-1 today with complimentary express delivery, 2-year warranty, and 30-day money-back guarantee.</p>
        <button class="btn-order" style="padding: 0.8rem 2.2rem; font-size: 1.05rem;" onclick="triggerCart()">Claim Your Vortex X-1 &bull; $89</button>
    </section>

    <!-- Toast -->
    <div class="toast" id="cartToast">
        <svg width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="9" cy="21" r="1"/><circle cx="20" cy="21" r="1"/><path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6"/></svg>
        <div>
            <strong>Added to Cart!</strong>
            <div style="font-size: 0.85rem; color: var(--text-secondary);">VORTEX X-1 Wireless Mouse reserved.</div>
        </div>
    </div>

    <!-- Footer -->
    <footer>
        <p>&copy; 2026 VORTEX Precision Technologies Inc. All rights reserved.</p>
        <div class="footer-badge">
            Powered by ASP.NET Core (.NET 10.0) on Azure App Service
        </div>
    </footer>

    <script>
        function setMouseColor(theme, el) {
            document.querySelectorAll('.color-dot').forEach(d => d.classList.remove('active'));
            el.classList.add('active');

            const chassis = document.getElementById('mouseChassis');
            const wheel = document.getElementById('scrollWheel');
            const ribs = document.getElementById('scrollRibs');
            const glow = document.getElementById('rgbUnderglow');

            if (theme === 'polar') {
                chassis.style.background = 'radial-gradient(circle at 50% 30%, #e2e8f0 0%, #cbd5e1 70%)';
                chassis.style.boxShadow = '0 25px 60px rgba(0, 0, 0, 0.6), 0 0 50px rgba(255, 255, 255, 0.4)';
                wheel.style.borderColor = '#38bdf8';
                ribs.style.background = '#38bdf8';
                glow.style.background = '#38bdf8';
                glow.style.boxShadow = '0 0 25px 5px #38bdf8';
            } else if (theme === 'cyber') {
                chassis.style.background = 'radial-gradient(circle at 50% 30%, #201335 0%, #0d081a 70%)';
                chassis.style.boxShadow = '0 25px 60px rgba(0, 0, 0, 0.6), 0 0 50px rgba(142, 45, 226, 0.6)';
                wheel.style.borderColor = '#e040fb';
                ribs.style.background = '#e040fb';
                glow.style.background = '#e040fb';
                glow.style.boxShadow = '0 0 25px 5px #e040fb';
            } else {
                chassis.style.background = 'radial-gradient(circle at 50% 30%, #1c2635 0%, #0d121a 70%)';
                chassis.style.boxShadow = '0 25px 60px rgba(0, 0, 0, 0.6), 0 0 50px rgba(0, 242, 254, 0.35)';
                wheel.style.borderColor = '#00f2fe';
                ribs.style.background = '#00f2fe';
                glow.style.background = '#00f2fe';
                glow.style.boxShadow = '0 0 25px 5px #00f2fe';
            }
        }

        function updateDpi(val) {
            const num = parseInt(val).toLocaleString();
            document.getElementById('dpiValue').innerText = num + ' DPI';
            
            const note = document.getElementById('dpiNote');
            if (val <= 1200) {
                note.innerText = 'Ideal for: Ultra-precise sniper scopes & tactical counter-strike gameplay.';
            } else if (val <= 4000) {
                note.innerText = 'Ideal for: Balanced fast-paced MOBA & competitive battle-royale tracking.';
            } else {
                note.innerText = 'Ideal for: Ultra-high speed 4K / 8K multi-monitor productivity & twitch reactions.';
            }
        }

        function triggerCart() {
            const toast = document.getElementById('cartToast');
            toast.style.display = 'flex';
            setTimeout(() => {
                toast.style.display = 'none';
            }, 3500);
        }
    </script>
</body>
</html>
""", "text/html"));

app.MapGet("/courses", () =>
{
    try
    {
        var courses = new List<object>();

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var sql = "SELECT CourseId, CourseName, Rating FROM Course";
        using var command = new SqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            courses.Add(new
            {
                CourseId = reader.GetInt32(0),
                CourseName = reader.GetString(1),
                Rating = reader.GetDecimal(2)
            });
        }

        return Results.Ok(courses);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message, title: "Database connection/query failed");
    }
});

app.Run();

