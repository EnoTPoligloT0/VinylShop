import React from "react";
import {Categories, Hero, Header, Banner, Popular} from "@/components";

export default function Home() {
  return (
    <main className={"overflow-hidden"}>
        <Header/>
      <Hero/>
        <Banner/>
        <Categories/>
        <Popular/>
    </main>
  );
}
