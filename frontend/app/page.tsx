import React from "react";
import {Categories, Hero, Header, Banner, Popular, Footer} from "@/components";

export default function Home() {
  return (
    <main className={"overflow-hidden"}>
      <Hero/>
        <Banner/>
        <Categories/>
        <Popular/>
    </main>
  );
}
