import {Hero} from "@/components";
import React from "react";
import {Header} from "@/components";

export default function Home() {
  return (
    <main className={"overflow-hidden"}>
        <Header/>
      <Hero/>
    </main>
  );
}
